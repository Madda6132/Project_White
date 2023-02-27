using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using RPG.Creature;
using RPG.World;
using RPG.Task;
using RPG.Task.Actions;

namespace RPG.Core {
    public class AreaHandler : MonoBehaviour {

        public static AreaHandler AreaHandlerInstance { 
            get {
                if(_Instance == null) { Debug.LogError("AreaHandler Instance is null. Instantiate an AreaHandler. AreaHandler should be in Core prefab"); }
                return _Instance;
            } 
            private set {
                _Instance = value;
            } 
        }
        static AreaHandler _Instance;

        //Player position display AbstractArea


        /// <summary>
        /// Get Path From first character to other character
        /// </summary>
        /// <param name="fromCharacter"> Path from this character </param>
        /// <param name="toCharacter"> Target character </param>
        public static IEnumerable<TaskMove> GetPathFromTo(Character fromCharacter, Character toCharacter) => GetPath(fromCharacter.Location, toCharacter.Location);
        /// <summary>
        /// Get Path from character to area
        /// </summary>
        /// <param name="fromCharacter"> Path from this character </param>
        /// <param name="toArea"> target area </param>
        public static IEnumerable<TaskMove> GetPathFromTo(Character fromCharacter, AbstractArea toArea) => GetPath(fromCharacter.Location, toArea);
        /// <summary>
        /// Get path from area to area
        /// </summary>
        /// <param name="fromArea"> From this path </param>
        /// <param name="toArea"> To this path </param>
        public static IEnumerable<TaskMove> GetPathFromTo(AbstractArea fromArea, AbstractArea toArea) => GetPath(fromArea, toArea);


        /*---Private---*/

        private void Awake() {

            if (_Instance != null) {
                Destroy(this);
                Debug.LogError("Cant have more than one instance of AreaHandler");
                return;
            }

            AreaHandlerInstance = this;
        }

        /// <summary>
        /// Get the area path to take to get to location
        /// </summary>
        /// <param name="start"> Path were to start from </param>
        /// <param name="end"> Path were to end </param>
        private static IEnumerable<TaskMove> GetPath(AbstractArea fromArea, AbstractArea toArea) {

            if (fromArea != toArea) {
                List<AbstractArea> travelPathList = CreatePathFromStartToFinish(fromArea, toArea);
                AbstractArea exploreFrom = fromArea;
                foreach (var location in travelPathList) {
                    switch (location is AbstractExploreArea ? location : exploreFrom) {

                        case AbstractMapArea:
                            yield return TaskActionLibrary.TravelTask(exploreFrom, location); //exploreFrom.CreateMoveTask(location);
                            exploreFrom = location;
                            break;

                        case AbstractExploreArea:
                            foreach (var explore in ExploreArea(exploreFrom, location)) {
                                yield return TaskActionLibrary.TravelTask(exploreFrom, explore); //exploreFrom.CreateMoveTask(explore);
                                exploreFrom = explore;
                            }
                            break;

                        default:
                            Debug.Log("Path finder from start found a connection it doesn't recognize " + location);
                            yield return null;
                            break;
                    }
                }
            }
        }

        //private static IEnumerable<TaskMove> GetPath(AbstractArea fromArea, AbstractArea toArea) {

        //    if (fromArea != toArea) {
        //        List<AbstractArea> travelPathList = CreatePathFromStartToFinish(fromArea, toArea);
        //        AbstractArea exploreFrom = fromArea;
        //        foreach (var location in travelPathList) {
        //            switch (location is AbstractExploreArea ? location : exploreFrom) {

        //                case AbstractMapArea:
        //                    yield return exploreFrom.CreateMoveTask(location);
        //                    exploreFrom = location;
        //                    break;

        //                case AbstractExploreArea:
        //                    foreach (var explore in ExploreArea(exploreFrom, location)) {
        //                        yield return exploreFrom.CreateMoveTask(explore);
        //                        exploreFrom = explore;
        //                    }
        //                    break;

        //                default:
        //                    Debug.Log("Path finder from start found a connection it doesn't recognize " + location);
        //                    yield return null;
        //                    break;
        //            }
        //        }
        //    }
        //}



        private static List<AbstractArea> CreatePathFromStartToFinish(AbstractArea fromArea, AbstractArea toArea) {
            //Get the path from start to end
            List<AbstractArea> startWorldLocation = new(fromArea.WorldLocation);
            List<AbstractArea> endWorldLocation = new(toArea.WorldLocation);

            //Remove start as it's not needed
            startWorldLocation.Remove(fromArea);

            //Find Link if any
            while (true) {
                if(RemoveOtherLinkAndContinue(ref startWorldLocation, ref endWorldLocation)) {
                    continue;
                }

                break;
            }

            //The AbstractBuilding breaks the logic in looking at the diffidence so a check is required to fix this issue
            //Check if Link is building and if end destination is targeting building
            if (endWorldLocation[0] is AreaBuilding && 1 < endWorldLocation.Count) {
                startWorldLocation.RemoveAt(0);
            }
            endWorldLocation.RemoveAt(0);

            //Flip the list upside down to construct travelList from start to finish
            startWorldLocation.Reverse();

            List<AbstractArea> travelList = new(startWorldLocation);
            travelList.AddRange(endWorldLocation);
            return travelList;
        }


        private static bool RemoveOtherLinkAndContinue(ref List<AbstractArea> startWorldLocation, ref List<AbstractArea> endWorldLocation) {
            if (startWorldLocation.Count < 2 || endWorldLocation.Count < 2) return false;

            AbstractArea startArea = startWorldLocation[1];
            AbstractArea endArea = endWorldLocation[1];
            if (startArea == endArea) {
                startWorldLocation.Remove(startWorldLocation[0]);
                endWorldLocation.Remove(endWorldLocation[0]);
                return true;
            }

            return false;
        }



        private static IEnumerable<AbstractArea> ExploreArea(AbstractArea start, AbstractArea end) {

            //As there is no coordinates to calculate distance it will check how many times it check its surrounding for connections to travel.
            //Each time counts as distance to determine the closest path.
            Queue<AbstractArea> areasToExplore = new();
            List<(AbstractArea area, int distance)> exploredAreas = new();

            int disntace = 0;
            //Search from start
            areasToExplore.Enqueue(start);
            bool breakLoops = false;
            while (0 < areasToExplore.Count) {

                Queue<AbstractArea> areasLeftToExplore = new(areasToExplore);
                areasToExplore.Clear();

                while (0 < areasLeftToExplore.Count) {

                    AbstractArea area = areasLeftToExplore.Dequeue();
                    exploredAreas.Add((area, disntace));
                    breakLoops = FindAndAddConnections(area, end, exploredAreas, ref areasToExplore);
                    if (breakLoops) {
                        break;
                    }
                }

                if (areasToExplore.Count == 0 || breakLoops) {
                    break;
                }

                disntace++;
            }

            //Remove the start since the characters doesn't need to enter it
            exploredAreas.Remove((start, 0));

            //Place the highest first on the list and check if it found what it was looking for
            exploredAreas.Reverse();
            if (exploredAreas.Count == 0 || exploredAreas[0].area != end) { 
                Debug.Log("Path not found"); 
                yield return null; 
            }

            Stack<(AbstractArea area, int distance)> foundPath = new();
            foundPath.Push(exploredAreas[0]);
            disntace--;


            foreach (var area_Distance in exploredAreas) {
                if (foundPath.Peek().distance < area_Distance.distance) {
                    continue;
                }

                AbstractExploreArea[] connections = (area_Distance.area).AreaConnections.
                    Where(z => exploredAreas.Contains((z, disntace))).      //Connection has been explored
                    Where(w => !ContainsValue(foundPath, w)).               //Check if path has already been added
                    Where(x => x is AbstractExploreArea).                   //It has to be explore area
                    Select(y => y as AbstractExploreArea).ToArray();        //Transform info explore area

                if (connections.Length == 0) {
                    continue;
                }

                foundPath.Push((connections[0], disntace));
                disntace--;
            }

            while (0 < foundPath.Count) {
                yield return foundPath.Pop().area;
            }
        }




        private static bool FindAndAddConnections(
            AbstractArea area,
            AbstractArea end,
            List<(AbstractArea area, int distance)> exploredAreas,
            ref Queue<AbstractArea> areasToExplore) {

            //Found the path
            if (area == end) {
                return true;
            }

            //Queue all connections
            if (area.AreaConnections.Length == 0) {
                return false;
            }

            AbstractArea[] filter = area.AreaConnections.
                Where(x => !ContainsValue(exploredAreas, x)).
                Where(y => y is AbstractExploreArea || y is AreaBuilding).ToArray();

            foreach (var connection in filter) {

                areasToExplore.Enqueue(connection);
            }

            return false;
        }



        private static bool ContainsValue(IEnumerable<(AbstractArea area, int distance)> list, AbstractArea area) {

            foreach (var checkValue in list) {

                if (checkValue.area == area) {
                    return true;
                }
            }

            return false;
        }
    }
}
