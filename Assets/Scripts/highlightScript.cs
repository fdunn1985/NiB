using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class highlightScript : MonoBehaviour {

    public int gridSize;
    public float scaleSize;
    public GameObject[] cells;
    public GameObject[] highlightCells;
    public GameObject num1Prefab;
    public GameObject num2Prefab;
    public GameObject num3Prefab;
    public GameObject num4Prefab;
    public GameObject num5Prefab;
    public GameObject num6Prefab;

    public GameObject num1GrayPrefab;
    public GameObject num2GrayPrefab;
    public GameObject num3GrayPrefab;
    public GameObject num4GrayPrefab;
    public GameObject num5GrayPrefab;
    public GameObject num6GrayPrefab;
    private List<Tile> tiles = new List<Tile>();

    void Start() { 
        //Game name is NiB = Numbers in Boxes!
        NewGame();
    }

    public void NewGame() {
        Text timerText = GameObject.Find("TimerText").GetComponent<Text>();
        TimerScript timerScript = (TimerScript)timerText.GetComponent(typeof(TimerScript));
        timerScript.ResetTimer();
        ClearNumInTile(tiles);
        tiles = new List<Tile>();

        Canvas pauseCanvas = GameObject.Find("Pause Canvas").GetComponent<Canvas>();
        pauseCanvas.enabled = false;
        Canvas youWinCanvas = GameObject.Find("You Win Canvas").GetComponent<Canvas>();
        youWinCanvas.enabled = false;

        foreach (GameObject obj in cells) {
            string[] name = obj.name.Split('_');
            
            foreach (GameObject highlight in highlightCells)
            {
                string[] highlightName = highlight.name.Split('_');
                if (name[1].Equals(highlightName[1]))
                {
                    Tile tile = new Tile(obj, highlight);
                    tile.isDisplayed = false;
                    tiles.Add(tile);
                    break;
                }
            }
        }
        setHighlights();

        GenerateMap genMapGenerator = new GenerateMap();
        genMapGenerator.generateMap(gridSize);
        Debug.Log(genMapGenerator);
        //Debug.Log(genMapGenerator.PrintSectionsToString());

        SetMapPointsToTiles(genMapGenerator.map);
        AssignColorsToGrid();
        SelectRandomNumbersToDisplay(gridSize);

    }

    void SelectRandomNumbersToDisplay(int size) {
        int[] randNumCount = new int[size];
        int[] randSectCount = new int[size];
        for (int i = 0; i < size; i++) {
            randNumCount[i] = 0;
            randSectCount[i] = 1;
        }

        for (int i = 0; i < size; i++) {
            SelectRandomFromSectionId(i+1, randNumCount);
        }

        int extraAmountToGen = 0;
        Debug.Log("Size: " + size);

        switch (size) {
            case 4: extraAmountToGen = 3;
                break;
            case 5: extraAmountToGen = 5;
                break;
        }

        for (int i=0; i<extraAmountToGen; i++) {
            int randNum = 0;
            do {
                randNum = UnityEngine.Random.Range(0, size);
            } while (randSectCount[randNum] > 3);
            SelectRandomFromSectionId(randNum + 1, randNumCount);
            randSectCount[randNum]++;
        }
    }

    void SelectRandomFromSectionId(int sectionId, int[] randNumCount) {
        List<Tile> tilePointList = new List<Tile>();

        foreach (Tile t in tiles) {
            if (t.mapPoint.sectionId.Equals(sectionId)) {
                tilePointList.Add(t);
            }
        }
        bool notDuplicate = false;
        do {
            int randNum = UnityEngine.Random.Range(0, tilePointList.Count);

            if (!tilePointList[randNum].isDisplayed /*&& randNumCount[tilePointList[randNum].mapPoint.numVal-1] < 3*/) {
                tilePointList[randNum].isDisplayed = true;
                DisplayNumInTile(tilePointList[randNum], tilePointList[randNum].mapPoint.numVal);
                randNumCount[tilePointList[randNum].mapPoint.numVal - 1]++;
                notDuplicate = true;
            }

        } while (!notDuplicate);
    }

    void DisplayNumInTile(Tile tile, int num) {
        Vector2 tilePos = tile.gameObj.transform.position;
        switch (num) {
            case 1:
                tile.numObj = Instantiate(num1GrayPrefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                tile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                tile.numVal = num;
                break;
            case 2:
                tile.numObj = Instantiate(num2GrayPrefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                tile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                tile.numVal = num;
                break;
            case 3:
                tile.numObj = Instantiate(num3GrayPrefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                tile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                tile.numVal = num;
                break;
            case 4:
                tile.numObj = Instantiate(num4GrayPrefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                tile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                tile.numVal = num;
                break;
            case 5:
                tile.numObj = Instantiate(num5GrayPrefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                tile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                tile.numVal = num;
                break;
            case 6:
                tile.numObj = Instantiate(num6GrayPrefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                tile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                tile.numVal = num;
                break;
        }
    }

    void ClearNumInTile(List<Tile> tiles) {
        foreach (Tile tile in tiles) { 
            Destroy(tile.numObj);
            tile.numVal = 0;
        }
    }

    List<Color> SetColorList() {
        Color red = new Color((242f / 255f), (82f / 255f), (82f / 255f));
        Color orange = new Color((244f / 255f), (177f / 255f), (83f / 255f));
        Color yellow = new Color((239f / 255f), (229f / 255f), (165f / 255f));
        Color green = new Color((106f / 255f), (247f / 255f), (108f / 255f));
        Color blue = new Color((106f / 255f), (242f / 255f), (247f / 255f));
        Color indigo = new Color((106f / 255f), (134f / 255f), (247f / 255f));
        Color violet = new Color((155f / 255f), (68f / 255f), (255f / 255f));
        Color white = new Color(1f,1f,1f);
        Color pink = new Color((255f/255f), (68f/255f), (248f/255f));
        Color brown = new Color((168f/255f), (146f/255f), (117f/255f));

        List<Color> colorList = new List<Color>();
        colorList.Add(red);
        colorList.Add(orange);
        colorList.Add(yellow);
        colorList.Add(green);
        colorList.Add(blue);
        colorList.Add(indigo);
        colorList.Add(violet);
        colorList.Add(white);
        colorList.Add(pink);
        colorList.Add(brown);

        return colorList;
    }

    List<int> SetColorAssignments(List<Color> colorList) {
        List<int> colorAssignments = new List<int>();

        List<int> colorIndexValues = new List<int>(); 
        for (int i=0; i<colorList.Count; i++) {
            colorIndexValues.Add(i);
        }

        List<int> indexClone = new List<int>(colorIndexValues);
        for (int i=0; i<colorIndexValues.Count; i++) {
            int randNum = UnityEngine.Random.Range(0, indexClone.Count);
            colorAssignments.Add(indexClone[randNum]);
            indexClone.RemoveAt(randNum);
        }

        return colorAssignments;
    }
    
    void SetMapPointsToTiles(MapPoint[,] map) {
        int tileCounter = 0;

        for (int r = 0; r < map.GetLength(0); r++) {
            for (int c = 0; c < map.GetLength(1); c++) {
                tiles[tileCounter].mapPoint = map[r, c];
                tileCounter++;
            }
        }
    }

    string printColorAssignment(List<int> myList) {
        string output = "";

        foreach (int i in myList) {
            output += " " + i;
        }

        return output;
    }

    void AssignColorsToGrid() {
        List<Color> colorList = SetColorList();
        List<int> colorAssignments = SetColorAssignments(colorList);
        string colorAssignmentsCsv = printColorAssignment(colorAssignments);
        //Debug.Log(colorAssignmentsCsv);
        foreach (Tile t in tiles) {
            SpriteRenderer sr = t.gameObj.GetComponent<SpriteRenderer>();
            sr.color = colorList[colorAssignments[t.mapPoint.sectionId-1]];
        }
    }

    // Update is called once per frame
    void Update () {
        if (!EventSystem.current.IsPointerOverGameObject()) {
        
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 1000); 
            
                if (hit) {
                    if (hit.collider.tag == "clickableCell") {
                        foreach(Tile tile in tiles) {
                            if (tile.gameObj.Equals(hit.collider.gameObject)) {
                                tile.isSelected = true;
                            } else {
                                tile.isSelected = false;
                            }
                        }
                        setHighlights();
                    } else if (hit.collider.tag == "clickableNum") {
                        string[] name = hit.collider.gameObject.name.Split('_');
                        PlaceNumberInCell(name[1]);
                    } else if (hit.collider.tag == "eraseCell") {
                        EraseCell();
                    }
                }
            }
        }
    }

    private void EraseCell() {
        Tile selectedTile = null;
        bool selectedObjExists = false;

        foreach (Tile tile in tiles) {
            if (tile.isSelected && !tile.isDisplayed) {
                selectedTile = tile;
                selectedObjExists = true;
                break;
            }
        }

        if (selectedObjExists) {
            Destroy(selectedTile.numObj);
            selectedTile.numVal = 0;
        }
    }

    private void PlaceNumberInCell(string numStr)
    {
        int num = Int16.Parse(numStr);
        Tile selectedTile = null;
        bool selectedObjExists = false;

        foreach (Tile tile in tiles) {
            if (tile.isSelected && !tile.isDisplayed) {
                selectedTile = tile;
                selectedObjExists = true;
                break;
            }
        }

        if (selectedObjExists) {
            Vector2 tilePos = selectedTile.gameObj.transform.position;

            if (selectedTile.numVal != 0)
            {
                Destroy(selectedTile.numObj);
            }
            switch (num)
            {
                case 1:
                    selectedTile.numObj = Instantiate(num1Prefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                    selectedTile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                    selectedTile.numVal = num;
                    break;
                case 2:
                    selectedTile.numObj = Instantiate(num2Prefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                    selectedTile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                    selectedTile.numVal = num;
                    break;
                case 3:
                    selectedTile.numObj = Instantiate(num3Prefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                    selectedTile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                    selectedTile.numVal = num;
                    break;
                case 4:
                    selectedTile.numObj = Instantiate(num4Prefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                    selectedTile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                    selectedTile.numVal = num;
                    break;
                case 5:
                    selectedTile.numObj = Instantiate(num5Prefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                    selectedTile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                    selectedTile.numVal = num;
                    break;
                case 6:
                    selectedTile.numObj = Instantiate(num6Prefab, new Vector3(tilePos.x, tilePos.y, 0), Quaternion.identity);
                    selectedTile.numObj.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
                    selectedTile.numVal = num;
                    break;
            }
        }

        CheckIfGameWon();
    }

    //TODO
    private void CheckIfGameWon() {
        Text timerText = GameObject.Find("TimerText").GetComponent<Text>();

        if (AllSpacesFilled() && !AllValuesCorrect()) {
            Debug.Log("there are incorrect values");
        } else if (AllSpacesFilled()&& AllValuesCorrect()) {
            Debug.Log("You Win!");

            //Call this to stop timer when game is over
            TimerScript ts = (TimerScript)timerText.GetComponent(typeof(TimerScript));
            ts.StopTimerGameOver();
            Debug.Log("It took you " + timerText.text + " to  solve");

            Canvas youWinCanvas = GameObject.Find("You Win Canvas").GetComponent<Canvas>();
            youWinCanvas.enabled = true;
            Text youWinTimeText = GameObject.Find("YouWinTimerText").GetComponent<Text>();
            youWinTimeText.text = "It took you " + timerText.text + "!";
        }


    }

    private bool AllSpacesFilled() {
        bool status = true;

        foreach (Tile t in tiles) {
            if (t.numVal == 0) {
                status = false;
                break;
            }
        }

        return status;
    }

    private bool AllValuesCorrect() {
        bool status = true;

        foreach(Tile t in tiles) {
            if (t.numVal != t.mapPoint.numVal) {
                status = false;
                break;
            }
        }

        return status;
    }

    private void setHighlights() {
        foreach(Tile tile in tiles) {
            if (!tile.isSelected) {
                tile.highlightObj.SetActive(false);
            } else {
                tile.highlightObj.SetActive(true);
            }
        }
    }
}
