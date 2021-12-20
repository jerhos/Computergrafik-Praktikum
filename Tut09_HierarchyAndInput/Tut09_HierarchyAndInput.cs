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
    [FuseeApplication(Name = "Tut09_HierarchyAndInput", Description = "Yet another FUSEE App.")]
    public class Tut09_HierarchyAndInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private float _camAngle;
        private float[] _keyArray = new float[5];
        private bool _wasPressed = false;
        private Transform _baseTransform;
        private Transform _bodyTransform;
        private Transform _upperArmTransform;
        private Transform _lowerArmTransform;
        private Transform _fingerTransform1;
        private Transform _fingerTransform2;
        private Transform _fingerTransform3;


       SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };
            _bodyTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Translation = new float3(0, 6, 0)
            };
            _upperArmTransform = new Transform
            {
                Translation = new float3(2, 4, 0)
            };
            _lowerArmTransform = new Transform
            {
               Translation = new float3(-2, 4, 0) 
            };
            _fingerTransform1 = new Transform
            {
                Translation = new float3(0, 4.5f, 0.5f)
            };
            _fingerTransform2 = new Transform
            {
                Translation = new float3(0.5f, 4.5f, -0.5f)
            };
            _fingerTransform3 = new Transform
            {
                Translation = new float3(-0.5f, 4.5f, -0.5f)
            };

            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNode>
                {
                    new SceneNode
                    {
                        Components = new List<SceneComponent>
                        {
                            // TRANSFORM COMPONENT
                            _baseTransform,

                            // SHADER EFFECT COMPONENT
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey),

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        }
                    },
                    // Red Body
                    new SceneNode
                    {
                        Components = 
                        {
                            _bodyTransform,
                            MakeEffect.FromDiffuseSpecular((float4)ColorUint.Red),
                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                        },
                        Children =
                        {
                            // Green Upper Arm Pivot Point
                            new SceneNode
                            {
                                Components = 
                                {
                                    _upperArmTransform
                                },
                                Children = 
                                {
                                    // Green Upper Arm is translated relative to its pivot point
                                    new SceneNode
                                    {
                                        Components =
                                        {
                                            new Transform {Translation = new float3(0, 4, 0)},
                                            MakeEffect.FromDiffuseSpecular((float4)ColorUint.Green),
                                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                        },
                                        Children = 
                                        {
                                            //Blue Lower Arm Pivot Point
                                            new SceneNode
                                            {
                                                Components = 
                                                {
                                                    _lowerArmTransform
                                                },
                                                Children = 
                                                {
                                                    // Blue Lower Arm is translated relative to its pivot point
                                                    new SceneNode
                                                    {
                                                        Components = 
                                                        {
                                                            new Transform {Translation = new float3(0, 4, 0)},
                                                            MakeEffect.FromDiffuseSpecular((float4)ColorUint.Blue),
                                                            SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                                        },
                                                        Children = 
                                                        {
                                                            // Orange Finger1 Pivot Point
                                                            new SceneNode
                                                            {
                                                                Components = 
                                                                {
                                                                    _fingerTransform1
                                                                },
                                                                Children = 
                                                                {
                                                                    // Orange Finger1 is translated relative to its pivot point
                                                                    new SceneNode
                                                                    {
                                                                        Components =
                                                                        {
                                                                            new Transform {Translation = new float3(0, 0.5f, 0)},
                                                                            MakeEffect.FromDiffuseSpecular((float4)ColorUint.Orange),
                                                                            SimpleMeshes.CreateCuboid(new float3(0.5f, 2.5f, 0.5f))
                                                                        }
                                                                    }
                                                                }
                                                            },
                                                            // Yellow Finger2 Pivot Point
                                                            new SceneNode
                                                            {
                                                                Components = 
                                                                {
                                                                    _fingerTransform2
                                                                },
                                                                Children = 
                                                                {
                                                                    // Yellow Finger2 is translated relative to its pivot point
                                                                    new SceneNode
                                                                    {
                                                                        Components =
                                                                        {
                                                                            new Transform {Translation = new float3(0, 0.5f, 0)},
                                                                            MakeEffect.FromDiffuseSpecular((float4)ColorUint.Yellow),
                                                                            SimpleMeshes.CreateCuboid(new float3(0.5f, 2.5f, 0.5f))
                                                                        }
                                                                    }
                                                                }
                                                            },
                                                            // Yellow Finger3 Pivot Point
                                                            new SceneNode
                                                            {
                                                                Components = 
                                                                {
                                                                    _fingerTransform3
                                                                },
                                                                Children = 
                                                                {
                                                                    // Yellow Finger3 is translated relative to its pivot point
                                                                    new SceneNode
                                                                    {
                                                                        Components =
                                                                        {
                                                                            new Transform {Translation = new float3(0, 0.5f, 0)},
                                                                            MakeEffect.FromDiffuseSpecular((float4)ColorUint.Yellow),
                                                                            SimpleMeshes.CreateCuboid(new float3(0.5f, 2.5f, 0.5f))
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }


        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to gray (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = (float4)ColorUint.LightBlue;

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();
            
            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            float _c = Time.DeltaTime * M.Pi * M.DegreesToRadians(45);
            KeyHandler();
            _camAngle +=_keyArray[0] * _c;

            // Setup the Robot
            _bodyTransform.Rotation = new float3(0, _bodyTransform.Rotation.y + _keyArray[1] * _c, 0);
            _upperArmTransform.Rotation = new float3(_upperArmTransform.Rotation.x + _keyArray[2] * _c, 0, 0);
            _lowerArmTransform.Rotation = new float3(_lowerArmTransform.Rotation.x + _keyArray[3] * _c, 0, 0);
            _fingerTransform1.Rotation = new float3(_fingerTransform1.Rotation.x + _keyArray[4] * _c * 4, 0, 0);
            _fingerTransform2.Rotation = new float3(_fingerTransform2.Rotation.x - _keyArray[4] * _c * 5, 0, 0);
            _fingerTransform3.Rotation = new float3(_fingerTransform3.Rotation.x - _keyArray[4] * _c * 5, 0, 0);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(M.DegreesToRadians(_camAngle));

            // Render the scene on the current render context
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

        public void KeyHandler()
        {        
            // Controls:
            /*
                Numpad:
                1-2 = Rotates the body
                4-7 = Rotates the upper arm
                5-8 = Rotates the lower arm
                0   = Grabbing
                Mouse:
                Leftclick & drag = Spins the view
            */
            
            // Reset all key presses
            for (int i = 1; i < _keyArray.Length; i++)
            {
                _keyArray[i] = 0;
            }
            // Spin camera on mouse drag
            if (Mouse.LeftButton)
            {
                _keyArray[0] = Mouse.Velocity.x;
            }
            // Body
            if (Keyboard.GetKey(KeyCodes.NumPad2))
            {
                _keyArray[1] = 2;
            }
            if (Keyboard.GetKey(KeyCodes.NumPad1))
            {
                _keyArray[1] = -2;
            }
            // Upper arm
            if (Keyboard.GetKey(KeyCodes.NumPad4))
            {
                _keyArray[2] = 2;
            }
            if (Keyboard.GetKey(KeyCodes.NumPad7))
            {
                _keyArray[2] = -2;
            }
        	// Lower arm
            if (Keyboard.GetKey(KeyCodes.NumPad5))
            {
                _keyArray[3] = 2;
            }
            if (Keyboard.GetKey(KeyCodes.NumPad8))
            {
                _keyArray[3] = -2;
            }
            // Fingers
            if (Keyboard.IsKeyDown(KeyCodes.NumPad0))
            {
                if (_wasPressed)
                {
                  _keyArray[4] = 1.5f;
                  _wasPressed = false;  
                } else
                {
                   _keyArray[4] = -1.5f;
                   _wasPressed = true;
                }    
            }
        }        
    }
}