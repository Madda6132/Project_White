using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Creature;
using RPG.World;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Linq;
using RPG.UI;
using RPG.UI.Library;


namespace RPG.Core {
    public class PlayerVisuals : MonoBehaviour {

        public static PlayerVisuals PlayerVisualsInstance {
            get {
                if (_Instance == null) { Debug.LogError("PlayerVisuals Instance is null. Instantiate an PlayerVisuals. PlayerVisuals should be in Core prefab"); }
                return _Instance;
            }
            private set {
                _Instance = value;
            }
        }


        [SerializeField] UILibrary _UILibrary;

        //Separated into layers
        [Header("Layer Structure")]
        //Background
        [Header("Background Layer")]
        
        [Tooltip("The Background Image")]
        [SerializeField] Image Background; //Usually used by the Area Image
        [Tooltip("Visuals display on background")]
        [SerializeField] Transform BackgroundVisuals; // Were everything is mostly displayed
        [Tooltip("Blur background and its visuals")]
        [SerializeField] Transform Blur; // out all visual in Background to focus on things in the front

        //Focus
        [Header("Focus Layer")]

        [Tooltip("Focus is in front of background layer")]
        [SerializeField] Transform Focus; //Were characters, dialogue bar, pop ups can be seen in front of blur screen 
        [Tooltip("Hide or show layers Focus and background")]
        [SerializeField] Transform FadeScreen; // Use for fading out 

        //Persistent
        [Header("Persistent Layer")]

        [Tooltip("Will be at the very front and wont be covered by Focus or background")]
        [SerializeField] Transform Persistent;

        //Prefab
        //Inventory
        //Buttons
        //Button vertical list
        //Stats -> Skills, Character sheet?

        //Test
        [Header("Test")]

        [SerializeField] UIChoiceList ChoiceList;

        Stack<string> categoryStack = new();
        string _CurrentCategory = BASE_CATEGORY;
        const string BASE_CATEGORY = "/";
        Dictionary<string, List<UIButtonManager>> _ButtonCategory = new();
        static PlayerVisuals _Instance;



        public static UILibrary UILibrary => _Instance?._UILibrary;

        public void UpdatePlayerVisuals(Character character) {

            AbstractArea area = character.Location;

            ChangeBackground(area.Background); 
        }
        


        public static void ButtonCategory_Back() {

            string category = 0 < _Instance.categoryStack.Count ? _Instance.categoryStack.Pop(): BASE_CATEGORY;
            ButtonCategory_UpdateCategory(category);
        }
        public static void ButtonCategory_SelectCategory(string category) {

            _Instance.categoryStack.Push(_Instance._CurrentCategory);
            ButtonCategory_UpdateCategory(category);
        }


        public void DisplayChoices(IController controller, IEnumerable<DisplayInfo_Button> UIDisplayInfo_ButtonCollection) {

            if (_Instance == null) return;

            //Clear buttons and dictionary
            var category = _Instance._ButtonCategory;
            foreach (var key in category.Keys) {
                category[key].ForEach(x => Destroy(x.gameObject));
                
            } 
            _Instance._ButtonCategory.Clear();
            //Add Path in list and
            //Remove all Character/Name/ from path
            string removeQuote = $"Character/{controller.Character.Name}";
            List<(string Path, DisplayInfo_Button Info)> taskList = new();
            foreach (var info in UIDisplayInfo_ButtonCollection) {

                string path = info.Path.Replace(removeQuote, "");
                taskList.Add(('/' + path, info));
            }
            

            //Sort by name
            taskList.OrderBy(x => x.Path);


            //For each Category create a Button object
            taskList.ForEach(x => {

                //Buttons for task and more category
                //Create category's
                if (!category.ContainsKey(x.Path)) CreateCategory(category, x.Path);

                //Create Button
                UIButtonManager taskButton = _Instance.ChoiceList.DisplayChoice(x.Info.DisplayName, x.Info.OnClickAction, x.Info.ButtonManager);

                category[x.Path].Add(taskButton);
                taskButton.gameObject.SetActive(false);
            });

            //PROTOTYPE: Add button for back
            UIButtonManager backButton = _Instance.ChoiceList.DisplayChoice("Back", () => ButtonCategory_Back(), 
                _Instance._UILibrary.UILibraryButtons.Default_Button);
            backButton.gameObject.SetActive(false);
            foreach (var key in category.Keys) {

                if (key == BASE_CATEGORY) continue;

                category[key].Add(backButton);
            }

            //Update buttons
            ButtonCategory_UpdateCategory(_Instance._CurrentCategory);
        }


        /*---Private---*/
        private void Awake() {

            if (_Instance != null) {
                Destroy(this);
                Debug.LogError("Cant have more than one instance of PlayerVisuals");
                return;
            }

            PlayerVisualsInstance = this;
            

        }


        public void ChangeBackground(Sprite background) {
            Background.sprite = background;
        }

        private static void ButtonCategory_UpdateCategory(string categoryPath) {


            //Check if _Instance._ButtonCategory contained the currentCategory
            //If not return to ""
            //Clear category stack and Update ""

            //If true open that category

            //Hide buttons not inside the category
            _Instance._ButtonCategory[_Instance._CurrentCategory].ForEach(x => x.gameObject.SetActive(false));

            if (_Instance._ButtonCategory.ContainsKey(categoryPath)) {

                //Change the Category label
                Debug.Log("Category: " + categoryPath);
                
                _Instance._CurrentCategory = categoryPath;
            } else {
                //Reset category selection 
                Debug.Log("Category doesn't exist");
                _Instance._CurrentCategory = BASE_CATEGORY;
                _Instance.categoryStack.Clear();
            }
            //Button list -> Category name at the top 
            _Instance.ChoiceList.DisplayLables(_Instance._CurrentCategory);
            //Show Buttons
            _Instance._ButtonCategory[_Instance._CurrentCategory].ForEach(x => x.gameObject.SetActive(true));
        }


        private void CharacterEntered(Character character) {
            Debug.Log(character.Name + " Entered :)");
            //Refresh task options
        }

        private void CharacterLeft(Character character) {
            Debug.Log(character.Name + " Left :(");
            //Refresh task options
        }

        private static void CreateCategory(Dictionary<string, List<UIButtonManager>> category, string path) {

            category.Add(path, new());

            string trackPath = path;
            if (path.Length < 2) return;

            string[] pathDetails = path.Split("/");

            //Separate Path into the layers

            //Remove last word and "/"
            int index = path.Length - pathDetails[pathDetails.Length - 2].Length - 1;
            string newCategory = path.Remove(index);

            //If it doesn't contain key create another category
            if (!category.ContainsKey(newCategory)) {
                CreateCategory(category, newCategory);
            }

            //Create the category button inside the path below targeted path
            UIButtonManager buttonCategory = _Instance.ChoiceList.DisplayChoice(pathDetails[pathDetails.Length - 2],
                () => ButtonCategory_SelectCategory(path), _Instance._UILibrary.UILibraryButtons.Category_Button);

            category[newCategory].Add(buttonCategory);
            buttonCategory.gameObject.SetActive(false);

        }

        private static UIButtonManager CreateButton(Transform parent, string displayText, UnityAction action, UIButtonManager buttonManager = null) {

            if (buttonManager == null) buttonManager = _Instance._UILibrary.UILibraryButtons.Default_Button;

            UIButtonManager button = Instantiate(buttonManager, parent);
            button.TMPro.text = displayText;
            button.Button.onClick.AddListener(action);

            return button;
        }

    }
}
