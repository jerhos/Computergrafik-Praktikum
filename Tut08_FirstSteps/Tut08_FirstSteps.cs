using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.Gui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut08_FirstSteps", Description = "Yet another FUSEE App.")]
    public class Tut08_FirstSteps : RenderCanvas
    {
        
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer; 
        private float _camAngle;
        
        // Transform components for the scene objects
        private Transform _cubeTransform_sun;
        private Transform _cubeTransform_mercury;
        private Transform _cubeTransform_venus;
        private Transform _cubeTransform_earth;
        private Transform _cubeTransform_moon;
        private Transform _cubeTransform_mars;
        private Transform _cubeTransform_jupiter;
        private Transform _cubeTransform_saturn;
        private Transform _cubeTransform_saturnsRings;
        private Transform _cubeTransform_uranus;
        private Transform _cubeTransform_neptune;
        
        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to Black
            RC.ClearColor = (float4) ColorUint.Black;

            // Set up instances of the Transform components
            _cubeTransform_sun = new Transform();
            _cubeTransform_mercury = new Transform();
            _cubeTransform_venus = new Transform();
            _cubeTransform_earth = new Transform();
            _cubeTransform_moon = new Transform();
            _cubeTransform_mars = new Transform();
            _cubeTransform_jupiter = new Transform();
            _cubeTransform_saturn = new Transform();
            _cubeTransform_saturnsRings = new Transform();
            _cubeTransform_uranus = new Transform();
            _cubeTransform_neptune = new Transform();

            // Did not work - ignore for now
            /*
            for(int i = 0; i < _cubeTransform.Length; i++) {
                _cubeTransform[i].Translation = new float3(0, 0, 0);
                _cubeTransform[i].Rotation = new float3(0, 0, 0);
                _cubeTransform[i].Scale = new float3(1, 1, 1);
            }
            */

            // Set up new Scene
            _scene = new SceneContainer 
            {
                Children = 
                {
                    // - Sceneobject
                    new SceneNode // Sonne
                    {
                        Components = 
                        {
                            _cubeTransform_sun,
                            MakeEffect.FromDiffuse((float4) ColorUint.Yellow),
                            SimpleMeshes.CreateCuboid(new float3(109.18f, 109.18f, 109.18f))
                        }
                    },
                    // - Sceneobject
                    new SceneNode // Mercury
                    {
                        Components = 
                        {
                            _cubeTransform_mercury,
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.SandyBrown),
                            SimpleMeshes.CreateCuboid(new float3(0.38f, 0.38f, 0.38f))
                        }
                    },
                    new SceneNode // Venus
                    {
                        Components = 
                        {
                            _cubeTransform_venus,
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.Orange),
                            SimpleMeshes.CreateCuboid(new float3(0.95f, 0.95f, 0.95f))
                        }
                    },
                    new SceneNode // Earth
                    {
                        Components = 
                        {
                            _cubeTransform_earth,
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.Blue),
                            SimpleMeshes.CreateCuboid(new float3(1, 1, 1))
                        }
                    },
                    new SceneNode // Moon
                    {
                        Components = 
                        {
                            _cubeTransform_moon,
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey),
                            SimpleMeshes.CreateCuboid(new float3(0.27f, 0.27f, 0.27f))
                        }
                    },
                    new SceneNode // Mars
                    {
                        Components = 
                        {
                            _cubeTransform_mars,
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.SandyBrown),
                            SimpleMeshes.CreateCuboid(new float3(0.53f, 0.53f, 0.53f))
                        }
                    },
                    new SceneNode // Jupiter
                    {
                        Components = 
                        {
                            _cubeTransform_jupiter,
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.SandyBrown),
                            SimpleMeshes.CreateCuboid(new float3(11.21f, 11.21f, 11.21f))
                        }
                    },
                    new SceneNode // Saturn
                    {
                        Components = 
                        {
                            _cubeTransform_saturn,
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.Brown),
                            SimpleMeshes.CreateCuboid(new float3(9.45f, 9.45f, 9.45f))
                        }
                    },
                    new SceneNode // Saturns Rings
                    {
                        Components = 
                        {
                            _cubeTransform_saturnsRings,
                            MakeEffect.FromDiffuse((float4) ColorUint.Gray),
                            SimpleMeshes.CreateCuboid(new float3(13.45f, 13.45f, 0.2f))
                        }
                    },
                    new SceneNode // Uranus
                    {
                        Components = 
                        {
                            _cubeTransform_uranus,
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightBlue),
                            SimpleMeshes.CreateCuboid(new float3(4.01f, 4.01f, 4.01f))
                        }
                    },
                    new SceneNode // Neptune
                    {
                        Components = 
                        {
                            _cubeTransform_neptune,
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.Blue),
                            SimpleMeshes.CreateCuboid(new float3(3.88f, 3.88f, 3.88f))
                        }
                    }
                }
            };

            
            // ignore for now
            /*
            _cubeTransform = new Transform {Translation = new float3(0, 0, 50), Rotation = new float3(0, 0.7f, 0)};

            var cubeShader = MakeEffect.FromDiffuseSpecular((float4) ColorUint.Blue);

            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));

            // 
            var cubeNode = new SceneNode();
            cubeNode.Components.Add(_cubeTransform);
            cubeNode.Components.Add(cubeShader);
            cubeNode.Components.Add(cubeMesh);

            _scene = new SceneContainer();
            _scene.Children.Add(cubeNode);
            */

            // Render next frame
            _sceneRenderer = new SceneRendererForward(_scene);
            
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);
            
           // Spin the camera - not used at the moment
           _camAngle = _camAngle - M.DegreesToRadians(25) * DeltaTime;
           

           // Position and Rotation of the camera
           RC.View = float4x4.CreateTranslation(-150, 0, 200) * float4x4.CreateRotationX(M.DegreesToRadians(-10f)) * float4x4.CreateRotationY(M.DegreesToRadians(-80));

           // Makes the cubes spin
           _cubeTransform_sun.Rotation =            _cubeTransform_sun.Rotation +           new float3(0, 10 * (3.141592f / 180) * Time.DeltaTime, 0);
           _cubeTransform_mercury.Rotation =        _cubeTransform_mercury.Rotation +       new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           _cubeTransform_venus.Rotation =          _cubeTransform_venus.Rotation +         new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           _cubeTransform_earth.Rotation =          _cubeTransform_earth.Rotation +         new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           _cubeTransform_moon.Rotation =           _cubeTransform_moon.Rotation +          new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           _cubeTransform_mars.Rotation =           _cubeTransform_mars.Rotation +          new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           _cubeTransform_jupiter.Rotation =        _cubeTransform_jupiter.Rotation +       new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           _cubeTransform_saturn.Rotation =         _cubeTransform_saturn.Rotation +        new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           _cubeTransform_saturnsRings.Rotation =   _cubeTransform_saturnsRings.Rotation +  new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           _cubeTransform_uranus.Rotation =         _cubeTransform_uranus.Rotation +        new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           _cubeTransform_neptune.Rotation =        _cubeTransform_neptune.Rotation +       new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           
           // Positions the cubes - (Keyboard.LeftRightAxis = moves the cubes when pressed)
           _cubeTransform_sun.Translation =             new float3(0 * Keyboard.LeftRightAxis, 0, 0);
           _cubeTransform_mercury.Translation =         new float3(45 * Keyboard.LeftRightAxis + 109 + 45.39f, 0, 0);
           _cubeTransform_venus.Translation =           new float3(2000 * Keyboard.LeftRightAxis + 109 + 84.82f, 0, 0);
           _cubeTransform_earth.Translation =           new float3(2000 * Keyboard.LeftRightAxis + 109 + 117.27f, 0, 0);
           _cubeTransform_moon.Translation =            new float3(2000 * Keyboard.LeftRightAxis + 110, 117.27f, 0);
           _cubeTransform_mars.Translation =            new float3(2000 * Keyboard.LeftRightAxis + 109 + 178.68f, 0, 0);
           _cubeTransform_jupiter.Translation =         new float3(2000 * Keyboard.LeftRightAxis + 109 + 610.21f, 0, 0);
           _cubeTransform_saturn.Translation =          new float3(2000 * Keyboard.LeftRightAxis + 109 + 1118.44f, 0, 0);
            _cubeTransform_saturnsRings.Translation =   new float3(2000 * Keyboard.LeftRightAxis + 109 + 1117.44f, 0, 0);
           _cubeTransform_uranus.Translation =          new float3(2000 * Keyboard.LeftRightAxis + 109 + 2250.63f, 0, 0);
           _cubeTransform_neptune.Translation =         new float3(2000 * Keyboard.LeftRightAxis + 109 + 3526.29f, 0, 0);
           
           
           // ignore
           /*
           // Rotation is added through a loop
           for(int i = 0; i < _cubeTransform.Length; i++) {
               _cubeTransform[i].Rotation = _cubeTransform[i].Rotation + new float3(0, 90 * (3.141592f / 180) * Time.DeltaTime, 0);
           }
           // Translation
           for(int i = 0; i < _cubeTransform.Length; i++) {
               _cubeTransform[i].Translation = new float3(10 * Keyboard.LeftRightAxis + 10 * i, 0, 50);
           }
            */
           // Diagnostics.Info(Keyboard.LeftRightAxis);
           // Diagnostics.Info(Time.DeltaTime);


           // Render Scene
           _sceneRenderer.Render(RC);
           
           // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }

        public void SetProjectionAndViewport()
        {
            // Set the rendering area to the entire window size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }        

    }
}