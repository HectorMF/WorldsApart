// Attach this script on your main camera

/* The MIT License (MIT)

Copyright (c) 2014, Marcel Căşvan

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */

using System;
using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraFit : MonoBehaviour
{
    #region FIELDS
    public float UnitsForWidth = 1; // width of your scene in unity units
    public Vector2 AspectRatio = new Vector2(16,9);
    public static CameraFit Instance;
    private DeviceOrientation orientation;
    private float _width;
    private float _height;
    //*** bottom screen
    private Vector3 _bl;
    private Vector3 _bc;
    private Vector3 _br;
    //*** middle screen
    private Vector3 _ml;
    private Vector3 _mc;
    private Vector3 _mr;
    //*** top screen
    private Vector3 _tl;
    private Vector3 _tc;
    private Vector3 _tr;
    #endregion

    #region PROPERTIES
    public float Width
    {
        get
        {
            return _width;
        }
    }
    public float Height
    {
        get
        {
            return _height;
        }
    }

    // helper points:
    public Vector3 BottomLeft
    {
        get
        {
            return _bl;
        }
    }
    public Vector3 BottomCenter
    {
        get
        {
            return _bc;
        }
    }
    public Vector3 BottomRight
    {
        get
        {
            return _br;
        }
    }
    public Vector3 MiddleLeft
    {
        get
        {
            return _ml;
        }
    }
    public Vector3 MiddleCenter
    {
        get
        {
            return _mc;
        }
    }
    public Vector3 MiddleRight
    {
        get
        {
            return _mr;
        }
    }
    public Vector3 TopLeft
    {
        get
        {
            return _tl;
        }
    }
    public Vector3 TopCenter
    {
        get
        {
            return _tc;
        }
    }
    public Vector3 TopRight
    {
        get
        {
            return _tr;
        }
    }
    #endregion

    #region METHODS
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;

        try
        {
            if ((bool)GetComponent<Camera>())
            {
                if (GetComponent<Camera>().orthographic)
                {
                    ComputeResolution();
                    ComputeAspectRatio();
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }

    private void Update()
    {
        #if UNITY_EDITOR
                ComputeAspectRatio();
                ComputeResolution();
        #endif
        if (Input.deviceOrientation != orientation)
        {
            if (orientation == DeviceOrientation.LandscapeLeft)
            {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            else if (orientation == DeviceOrientation.LandscapeRight)
            {
                Screen.orientation = ScreenOrientation.LandscapeRight;
            }
            ComputeResolution();
            ComputeAspectRatio();
            orientation = Input.deviceOrientation;

        }
    }

    private void ComputeResolution()
    {
        float deviceWidth;
        float deviceHeight;
        float leftX, rightX, topY, bottomY;

#if UNITY_EDITOR
        deviceWidth = GetGameView().x;
        deviceHeight = GetGameView().y;
#else
        deviceWidth = Screen.width;
        deviceHeight = Screen.height;
#endif

        GetComponent<Camera>().orthographicSize = 1f / GetComponent<Camera>().aspect * UnitsForWidth / 2f;

        _height = 2f * GetComponent<Camera>().orthographicSize;
        _width = _height * GetComponent<Camera>().aspect;

        float cameraX, cameraY;
        cameraX = GetComponent<Camera>().transform.position.x;
        cameraY = GetComponent<Camera>().transform.position.y;

        leftX = cameraX - _width / 2;
        rightX = cameraX + _width / 2;
        topY = cameraY + _height / 2;
        bottomY = cameraY - _height / 2;

        //*** bottom
        _bl = new Vector3(leftX, bottomY, 0);
        _bc = new Vector3(cameraX, bottomY, 0);
        _br = new Vector3(rightX, bottomY, 0);
        //*** middle
        _ml = new Vector3(leftX, cameraY, 0);
        _mc = new Vector3(cameraX, cameraY, 0);
        _mr = new Vector3(rightX, cameraY, 0);
        //*** top
        _tl = new Vector3(leftX, topY, 0);
        _tc = new Vector3(cameraX, topY, 0);
        _tr = new Vector3(rightX, topY, 0);
        Instance = this;
    }

    private void OnDrawGizmos()
    {
        if (GetComponent<Camera>().orthographic)
        {
            DrawGizmos();
        }
    }

    private void DrawGizmos()
    {
        //*** bottom
        Gizmos.DrawIcon(_bl, "point.png", false);
        Gizmos.DrawIcon(_bc, "point.png", false);
        Gizmos.DrawIcon(_br, "point.png", false);
        //*** middle
        Gizmos.DrawIcon(_ml, "point.png", false);
        Gizmos.DrawIcon(_mc, "point.png", false);
        Gizmos.DrawIcon(_mr, "point.png", false);
        //*** top
        Gizmos.DrawIcon(_tl, "point.png", false);
        Gizmos.DrawIcon(_tc, "point.png", false);
        Gizmos.DrawIcon(_tr, "point.png", false);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(_bl, _br);
        Gizmos.DrawLine(_br, _tr);
        Gizmos.DrawLine(_tr, _tl);
        Gizmos.DrawLine(_tl, _bl);
    }

    private Vector2 GetGameView()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo getSizeOfMainGameView =
        T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object resolution = getSizeOfMainGameView.Invoke(null, null);
        return (Vector2)resolution;
    }

    private void ComputeAspectRatio()
    {
        float targetaspect = AspectRatio.x / AspectRatio.y;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = GetComponent<Camera>().rect;
            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            GetComponent<Camera>().rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;
            Rect rect = GetComponent<Camera>().rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            GetComponent<Camera>().rect = rect;
        }
    }

    #endregion
}