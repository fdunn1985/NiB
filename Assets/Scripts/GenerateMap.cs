using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap {

    public MapPoint[,] map {get; set;}

    public void generateMap(int size)
    {
        map = new MapPoint[size, size];

        for (int row = 0; row < size; row++) {
            for (int col = 0; col < size; col++) {
                map[row, col] = new MapPoint(0, 0);
            }
        }

        List<int> numsToPick = new List<int>();
        for (int i=1; i <= size; i++)
        {
            numsToPick.Add(i);
        }
        
        for (int row = 0; row < size; row++) {
            List<int> numsClone = new List<int>(numsToPick);
            int numPos = 0;
            for (int col = 0; col < size; col++) {
                int counter = 0;
                do
                {
                    if (counter >=5)
                    {
                        for (int colReset=0; colReset < size; colReset++)
                        {
                            map[row, colReset].numVal = 0;
                        }
                        col = 0;
                        counter = 0;
                        numsClone = new List<int>(numsToPick);
                    }
                    numPos = UnityEngine.Random.Range(0, numsClone.Count);
                    map[row, col].numVal = numsClone[numPos];
                    map[row, col].rCoord = row;
                    map[row, col].cCoord = col;
                    counter++;
                } while (IsDuplicateInColumn(col));

                numsClone.RemoveAt(numPos);
                
            }
        }
        bool unassignedSectionExists = false;
        do {
            ResetSections(size);
            unassignedSectionExists = TraverseMapForSectionIds(size);
        } while (unassignedSectionExists);
            //TraverseMapForSectionIds(size);
    }
    
    private void CheckAndAssignValues(int currentSectionId, int size) {
        List<int> availableNumbers = new List<int>();
        List<MapPoint> pointList = new List<MapPoint>();
        for (int i = 1; i <= size; i++) {
            availableNumbers.Add(i);
        }

        
        for (int r = 0; r < size; r++) {
            for (int c = 0; c < size; c++) {
                MapPoint point = map[r, c];
                if (point.sectionId.Equals(currentSectionId)) {
                    availableNumbers.Remove(point.numVal);
                    pointList.Add(point);

                    RecursiveAssigningGrid(availableNumbers, pointList, currentSectionId);
                }
            }
        }
    }

    private void RecursiveAssigningGrid(List<int> availableNumbers, List<MapPoint> pointList, int sectionId) {
        if (availableNumbers.Count != 0) {
            
            List<MapPoint> pointsToCheck = new List<MapPoint>();
            foreach (MapPoint point in pointList) {
                int r = point.rCoord;
                int c = point.cCoord;

                if (r+1 < map.GetLength(0) && availableNumbers.Contains(map[r+1,c].numVal) && map[r+1,c].sectionId==0) {
                    pointsToCheck.Add(map[r+1,c]);
                }
                if (c+1 < map.GetLength(0) && availableNumbers.Contains(map[r,c+1].numVal) && map[r,c+1].sectionId==0) {
                    pointsToCheck.Add(map[r,c+1]);
                }
                if (c - 1 > -1 && availableNumbers.Contains(map[r, c - 1].numVal) && map[r, c - 1].sectionId == 0) {
                    pointsToCheck.Add(map[r, c - 1]);
                }
            }
            int randPointNum = 0;
            if (pointsToCheck.Count > 0) {
                if (pointsToCheck.Count > 1) {
                    randPointNum = UnityEngine.Random.Range(0, pointsToCheck.Count);
                }
                pointsToCheck[randPointNum].sectionId = sectionId;
                availableNumbers.Remove(pointsToCheck[randPointNum].numVal);
            }
        }
    }

    private bool TraverseMapForSectionIds(int size) {
        int currentSectionId = 1;
        for (int r = 0; r < size; r++) {
            for (int c = 0; c < size; c++) {
                MapPoint point = map[r, c];

                if (r == 0 && c == 0) {
                    point.sectionId = currentSectionId;
                    CheckAndAssignValues(currentSectionId, size);
                    currentSectionId++;
                } else {
                    if (point.sectionId == 0 && currentSectionId <= size) {
                        point.sectionId = currentSectionId;
                        CheckAndAssignValues(currentSectionId, size);
                        currentSectionId++;
                    }
                }
            }
        }

        //Check if any areas don't have a section
        bool unassignedSectionExists = false;
        for (int r = 0; r < size; r++) {
            for (int c = 0; c < size; c++) {
                MapPoint point = map[r, c];
                if (point.sectionId == 0) {
                    unassignedSectionExists = true;
                }
            }
        }

        return unassignedSectionExists;

        /*if (unassignedSectionExists) {
            ResetSections(size);
            TraverseMapForSectionIds(size);
        }*/
    }

    private void ResetSections(int size) {
        for (int r = 0; r < size; r++) {
            for (int c = 0; c < size; c++) {
                MapPoint point = map[r, c];
                point.sectionId = 0;
            }
        }
    }

    private bool IsDuplicateInColumn(int col) {
        bool status = false;

        for (int row1 = 0; row1 < map.GetLength(0)-1; row1++) {
            for (int row2 = row1 + 1; row2 < map.GetLength(0); row2++) { 
                if ((map[row1, col].numVal == map[row2, col].numVal) && map[row2,col].numVal != 0)
                {
                    status = true;
                    break;
                }
            }
        }

        return status;
    }

    public string PrintSectionsToString() {
        string output = "[";

        for (int row = 0; row < map.GetLength(0); row++) {
            output += "(";
            for (int col = 0; col < map.GetLength(1); col++) {
                output += map[row, col].sectionId + " ";
            }
            output += ")";
            if (row < map.GetLength(1) - 1) {
                output += "\n";
            }
        }

        output += "]";

        return output;
    }

    public override string ToString()
    {
        string output = "[";

        for (int row = 0; row < map.GetLength(0); row++) {
            output += "(";
            for (int col = 0; col < map.GetLength(1); col++) {
                output += map[row, col].numVal + " ";
            }
            output += ")";
            if (row < map.GetLength(1)-1)
            {
                output += "\n";
            }
        }

        output += "]";

        return output;
    }
	
}
