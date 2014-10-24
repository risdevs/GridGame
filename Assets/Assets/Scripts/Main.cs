//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//
using Gamelogic;
using Gamelogic.Grids;
using UnityEngine;
using UnityEngine.UI;

public class Main : GLMonoBehaviour
{
    // This is the prefab that will be used for each cell in the grid. 
    // Normally, you would use your own version that has information
    // related to your game.
    public SpriteCell cellPrefab;
    public UnitMover unitPrefab;
    public Toggle deployUnitPrefab;
    public Image UIRoot;

    // All cells will be parented to this object.
    public GameObject root;
    public GameObject units;
    
    // The grid data structure that contains all cell.
    private RectGrid<SpriteCell> grid;

    // The map (that converts between world and grid coordinates).
    private IMap3D<RectPoint> map;

    //Unit that moves with user click
    private UnitMover clickUnit;

    //Unit that moves automatically
    private UnitMover autoUnit;

    private const int MAP_W = 10;
    private const int MAP_H = 10;

    private Toggle[] deployButtons;
    private int selectedButton;

    public IMap3D<RectPoint> WorldMap
    {
        get
        {
            return map;
        }
    }

    public void Start()
    {       
        BuildGrid();
        BuildUI();

        clickUnit = Instantiate(unitPrefab);
        clickUnit.main = this;
        clickUnit.transform.parent = units.transform;

        autoUnit = Instantiate(unitPrefab);
        autoUnit.main = this;
        autoUnit.transform.parent = units.transform;
    }

    private void BuildUI()
    {
        selectedButton = -1;

        deployButtons = new Toggle[5];

        for (var i = 0; i < 5; i++)
        {
            Toggle tmp = Instantiate(deployUnitPrefab);
            RectTransform rt = (RectTransform) tmp.transform;
            tmp.transform.position = new Vector3(i * rt.rect.width, 0);
            tmp.transform.parent = UIRoot.transform;
            tmp.GetComponent<DeployUnitButton>().type = i;
            tmp.onValueChanged.AddListener(delegate(Toggle toggle)
            {
                //int a = tmp.GetComponent<DeployUnitButton>().type;
                if (toggle.isOn)
                {
                    if (selectedButton != -1)
                    {
                        deployButtons[selectedButton].isOn = false;
                    }
                    selectedButton = toggle.GetComponent<DeployUnitButton>().type;

                } else 
                {
                    selectedButton = -1;
                }
            });

            deployButtons [i] = tmp;
        }
    }

    private void BuildGrid()
    {
        // Creates a grid in a rectangular shape.
        grid = RectGrid<SpriteCell>.Rectangle(MAP_W, MAP_H);

        // Creates a map...
        map = new RectMap(cellPrefab.Dimensions)    // The cell dimensions usually correspond to the visual 
                                                    // part of the sprite in pixels. Here we use the actual 
                                                    // sprite size, which causes a border between cells.
                                                    // 
            .WithWindow(ExampleUtils.ScreenRect) // ...that is centered in the rectangle provided
            .AlignMiddleCenter(grid)             // by this and the previous line.
            .To3DXY();  // This makes the 2D map returned by the last function into a 3D map
        // This map assumes the grid is in the XY plane.
        // To3DXZ assumes the grid is in the XZ plane (you have to make sure 
        //your tiles are similarly aligned / rotated).


        foreach (RectPoint point in grid) //Iterates over all points (coordinates) contained in the grid
        {
            SpriteCell cell = Instantiate(cellPrefab); // Instantiate a cell from the given prefab.
            //This generic version of Instantiate is defined in GLMonoBehaviour
            //If you don't want to use GLMonoBehvaiour, you have to cast the result of
            //Instantiate

            Vector3 worldPoint = map [point];   //Calculate the world point of the current grid point

            cell.transform.parent = root.transform; //Parent the cell to the root
            cell.transform.localScale = Vector3.one;    //Readjust the scale - the re-parenting above may have changed it.
            cell.transform.localPosition = worldPoint;  //Set the localPosition of the cell.

            cell.FrameIndex = 0;
            cell.Color = new Color(1, 1, 1); //ExampleUtils.Colors[point.GetColor4() % 4 * 4]; //Sets the color of the cell
            //See http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/ for more information on colorings.

            cell.name = point.ToString(); // Makes it easier to identify cells in the editor.
            grid [point] = cell; // Finally, put the cell in the grid.
        }
    }
    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // If you use a different GUI system, you will probably need a 
            // custom version of this function.
            // This assumes your camera is orthographic. For perspective cameras,
            // you must use a ray casting method instead.
            Vector3 worldPosition = GridBuilderUtils.ScreenToWorld(root, Input.mousePosition);
            
            // Calculates the grid point that corresponds to the given world coordinate.
            RectPoint point = map [worldPosition];
            
            // The point may in fact lie outside the grid as we defined it when we built it.
            // So we first check whether the grid contains the point...
            if (grid.Contains(point))
            {
                //... and set the color of the cell at that point to white if it does.
                grid [point].OnClick();
                
                clickUnit.targetPoint = point;
            }
        }

        //If autoUnit has reach is destination, move it to the next tile
        if (autoUnit.hasReachedDestination())
        {
            autoUnit.targetPoint = (autoUnit.targetPoint.Y < MAP_H  -1 ?
                                    new RectPoint(autoUnit.targetPoint.X, autoUnit.targetPoint.Y + 1) :
                                    new RectPoint((autoUnit.targetPoint.X + 1) % MAP_W, 0));
        }
    }

    private bool onDeployButtonSelected(Toggle t)
    {

        return true;
    }
}