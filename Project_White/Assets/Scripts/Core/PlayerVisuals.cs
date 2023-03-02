using System.Collections.Generic;
using UnityEngine;
using RPG.Creature;
using RPG.World;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using RPG.UI;
using RPG.UI.Library;


namespace RPG.Core {
    public class PlayerVisuals : MonoBehaviour {


        [SerializeField] UILibrary uiLibrary;

        //Separated into layers
        [Header("Layer Structure")]
        //Background
        [Header("Background Layer")]
        
        [Tooltip("The Background Image")]
        [SerializeField] Image background; //Usually used by the Area Image
        [Tooltip("Visuals display on background")]
        [SerializeField] Transform backgroundVisuals; // Were everything is mostly displayed
        [Tooltip("Blur background and its visuals")]
        [SerializeField] Transform blur; // out all visual in Background to focus on things in the front

        //Focus
        [Header("Focus Layer")]

        [Tooltip("Focus is in front of background layer")]
        [SerializeField] Transform focus; //Were characters, dialogue bar, pop ups can be seen in front of blur screen 
        [Tooltip("Hide or show layers Focus and background")]
        [SerializeField] Transform fadeScreen; // Use for fading out 

        //Persistent
        [Header("Persistent Layer")]

        [Tooltip("Will be at the very front and wont be covered by Focus or background")]
        [SerializeField] Transform persistent;

        //Prefab
        //Inventory
        //Buttons
        //Button vertical list
        //Stats -> Skills, Character sheet?

        //Test
        [Header("Test")]

        [SerializeField] UIChoiceList choiceList;

        private static PlayerVisuals Instance {
            get {
                if (_instance == null) { Debug.LogError("PlayerVisuals Instance is null. Instantiate an PlayerVisuals. PlayerVisuals should be in Core prefab"); }
                return _instance;
            }
        }
        public static UILibrary UILibrary => Instance.uiLibrary;

        readonly Stack<string> _categoryStack = new();
        string _currentCategory = BaseCategory;
        const string BaseCategory = "/";
        readonly Dictionary<string, List<UIButtonManager>> _buttonCategory = new();

        static PlayerVisuals _instance;

        public static void UpdatePlayerVisuals(Character character) {
            AbstractArea area = character.Location;
            ChangeBackground(area.Background); 
        }
        


        public static void ButtonCategory_Back() {
            string category = 0 < Instance._categoryStack.Count ? Instance._categoryStack.Pop(): BaseCategory;
            ButtonCategory_UpdateCategory(category);
        }
        public static void ButtonCategory_SelectCategory(string category) {
            Instance._categoryStack.Push(Instance._currentCategory);
            ButtonCategory_UpdateCategory(category);
        }


        public static void DisplayChoices(IController controller, IEnumerable<DisplayInfoButton> uiDisplayInfoButtonCollection) {
            if (Instance == null) return;
            //Clear buttons and dictionary
            var category = Instance._buttonCategory;
            foreach (var key in category.Keys) {
                category[key].ForEach(x => Destroy(x.gameObject));
            }
            Instance._buttonCategory.Clear();
            //Add Path in list and
            //Remove all Character/Name/ from path
            string removeQuote = $"Character/{controller.Character.CharacterName}";
            List<(string Path, DisplayInfoButton Info)> taskList = new();
            foreach (var info in uiDisplayInfoButtonCollection) {

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
                UIButtonManager taskButton = Instance.choiceList.DisplayChoice(x.Info.DisplayName, x.Info.OnClickAction, x.Info.ButtonManager);
                category[x.Path].Add(taskButton);
                taskButton.gameObject.SetActive(false);
            });
            //PROTOTYPE: Add button for back
            UIButtonManager backButton = Instance.choiceList.DisplayChoice("Back", () => ButtonCategory_Back(),
                Instance.uiLibrary.UILibraryButtons.DefaultButton);
            backButton.gameObject.SetActive(false);
            foreach (var key in category.Keys) {

                if (key == BaseCategory) continue;

                category[key].Add(backButton);
            }
            //Update buttons
            ButtonCategory_UpdateCategory(Instance._currentCategory);
        }

        /*---Private---*/
        private void Awake() {
            if (_instance != null) {
                Destroy(this);
                Debug.LogError("Cant have more than one instance of PlayerVisuals");
                return;
            }
            _instance = this;
        }

        public static void ChangeBackground(Sprite background) {
            Instance.background.sprite = background;
        }

        private static void ButtonCategory_UpdateCategory(string categoryPath) {


            //Check if _Instance._ButtonCategory contained the currentCategory
            //If not return to ""
            //Clear category stack and Update ""

            //If true open that category

            //Hide buttons not inside the category
            Instance._buttonCategory[Instance._currentCategory].ForEach(x => x.gameObject.SetActive(false));

            if (Instance._buttonCategory.ContainsKey(categoryPath)) {

                //Change the Category label
                Debug.Log("Category: " + categoryPath);

                Instance._currentCategory = categoryPath;
            } else {
                //Reset category selection 
                Debug.Log("Category doesn't exist");
                Instance._currentCategory = BaseCategory;
                Instance._categoryStack.Clear();
            }
            //Button list -> Category name at the top 
            Instance.choiceList.DisplayLabels(Instance._currentCategory);
            //Show Buttons
            Instance._buttonCategory[Instance._currentCategory].ForEach(x => x.gameObject.SetActive(true));
        }

        private static void CreateCategory(Dictionary<string, List<UIButtonManager>> category, string path) {

            category.Add(path, new());

            string trackPath = path;
            if (path.Length < 2) return;

            string[] pathDetails = path.Split("/");

            //Separate Path into the layers

            //Remove last word and "/"
            System.Index secondLastIndex = ^2;
            int index = path.Length - pathDetails[secondLastIndex].Length - 1;
            string newCategory = path.Remove(index);

            //If it doesn't contain key create another category
            if (!category.ContainsKey(newCategory)) {
                CreateCategory(category, newCategory);
            }

            //Create the category button inside the path below targeted path
            UIButtonManager buttonCategory = Instance.choiceList.DisplayChoice(pathDetails[pathDetails.Length - 2],
                () => ButtonCategory_SelectCategory(path), Instance.uiLibrary.UILibraryButtons.CategoryButton);

            category[newCategory].Add(buttonCategory);
            buttonCategory.gameObject.SetActive(false);

        }

        private static UIButtonManager CreateButton(Transform parent, string displayText, UnityAction action, UIButtonManager buttonManager = null) {

            if (buttonManager == null) buttonManager = Instance.uiLibrary.UILibraryButtons.DefaultButton;

            UIButtonManager button = Instantiate(buttonManager, parent);
            button.TMPro.text = displayText;
            button.Button.onClick.AddListener(action);

            return button;
        }
    }
}
