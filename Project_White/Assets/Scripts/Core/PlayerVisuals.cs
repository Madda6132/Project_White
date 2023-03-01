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

        public static PlayerVisuals Instance {
            get {
                if (_Instance == null) { Debug.LogError("PlayerVisuals Instance is null. Instantiate an PlayerVisuals. PlayerVisuals should be in Core prefab"); }
                return _Instance;
            }
            private set {
                _Instance = value;
            }
        }
        public static UILibrary UILibrary => Instance._UILibrary;

        Stack<string> categoryStack = new();
        string _CurrentCategory = BASE_CATEGORY;
        const string BASE_CATEGORY = "/";
        Dictionary<string, List<UIButtonManager>> _ButtonCategory = new();

        static PlayerVisuals _Instance;

        public void UpdatePlayerVisuals(Character character) {
            AbstractArea area = character.Location;
            ChangeBackground(area.Background); 
        }
        


        public static void ButtonCategory_Back() {
            string category = 0 < Instance.categoryStack.Count ? Instance.categoryStack.Pop(): BASE_CATEGORY;
            ButtonCategory_UpdateCategory(category);
        }
        public static void ButtonCategory_SelectCategory(string category) {
            Instance.categoryStack.Push(Instance._CurrentCategory);
            ButtonCategory_UpdateCategory(category);
        }


        public void DisplayChoices(IController controller, IEnumerable<DisplayInfo_Button> UIDisplayInfo_ButtonCollection) {
            if (Instance == null) return;
            //Clear buttons and dictionary
            var category = Instance._ButtonCategory;
            foreach (var key in category.Keys) {
                category[key].ForEach(x => Destroy(x.gameObject));
            }
            Instance._ButtonCategory.Clear();
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
                UIButtonManager taskButton = Instance.ChoiceList.DisplayChoice(x.Info.DisplayName, x.Info.OnClickAction, x.Info.ButtonManager);
                category[x.Path].Add(taskButton);
                taskButton.gameObject.SetActive(false);
            });
            //PROTOTYPE: Add button for back
            UIButtonManager backButton = Instance.ChoiceList.DisplayChoice("Back", () => ButtonCategory_Back(),
                Instance._UILibrary.UILibraryButtons.Default_Button);
            backButton.gameObject.SetActive(false);
            foreach (var key in category.Keys) {

                if (key == BASE_CATEGORY) continue;

                category[key].Add(backButton);
            }
            //Update buttons
            ButtonCategory_UpdateCategory(Instance._CurrentCategory);
        }

        /*---Private---*/
        private void Awake() {
            if (_Instance != null) {
                Destroy(this);
                Debug.LogError("Cant have more than one instance of PlayerVisuals");
                return;
            }
            _Instance = this;
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
            Instance._ButtonCategory[Instance._CurrentCategory].ForEach(x => x.gameObject.SetActive(false));

            if (Instance._ButtonCategory.ContainsKey(categoryPath)) {

                //Change the Category label
                Debug.Log("Category: " + categoryPath);

                Instance._CurrentCategory = categoryPath;
            } else {
                //Reset category selection 
                Debug.Log("Category doesn't exist");
                Instance._CurrentCategory = BASE_CATEGORY;
                Instance.categoryStack.Clear();
            }
            //Button list -> Category name at the top 
            Instance.ChoiceList.DisplayLables(Instance._CurrentCategory);
            //Show Buttons
            Instance._ButtonCategory[Instance._CurrentCategory].ForEach(x => x.gameObject.SetActive(true));
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
            UIButtonManager buttonCategory = Instance.ChoiceList.DisplayChoice(pathDetails[pathDetails.Length - 2],
                () => ButtonCategory_SelectCategory(path), Instance._UILibrary.UILibraryButtons.Category_Button);

            category[newCategory].Add(buttonCategory);
            buttonCategory.gameObject.SetActive(false);

        }

        private static UIButtonManager CreateButton(Transform parent, string displayText, UnityAction action, UIButtonManager buttonManager = null) {

            if (buttonManager == null) buttonManager = Instance._UILibrary.UILibraryButtons.Default_Button;

            UIButtonManager button = Instantiate(buttonManager, parent);
            button.TMPro.text = displayText;
            button.Button.onClick.AddListener(action);

            return button;
        }
    }
}
