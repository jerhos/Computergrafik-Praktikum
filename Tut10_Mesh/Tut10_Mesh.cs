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
    [FuseeApplication(Name = "Tut10_Mesh", Description = "Yet another FUSEE App.")]
    public class Tut10_Mesh : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private Transform[] _baseTransform = new Transform[3];
        private float _camAngle;

        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform[0] = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };
            _baseTransform[1] = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(10, -3, 0)
            };
            _baseTransform[2] = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-9, 4, 0)
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
                            _baseTransform[0],

                            // SHADER EFFECT COMPONENT
                            SimpleMeshes.MakeMaterial((float4) ColorUint.Red),

                            // MESH COMPONENT
                            SimpleMeshes.CreateCylinder(5, 10, 16)
                        }
                    },
                    new SceneNode
                    {
                        Components = new List<SceneComponent>
                        {
                            // TRANSFORM COMPONENT
                            _baseTransform[1],

                            // SHADER EFFECT COMPONENT
                            SimpleMeshes.MakeMaterial((float4) ColorUint.Orange),

                            // MESH COMPONENT
                            SimpleMeshes.CreateCylinder(4, 7, 8)
                        }
                    },
                    new SceneNode
                    {
                        Components = new List<SceneComponent>
                        {
                            // TRANSFORM COMPONENT
                            _baseTransform[2],

                            // SHADER EFFECT COMPONENT
                            SimpleMeshes.MakeMaterial((float4) ColorUint.Yellow),

                            // MESH COMPONENT
                            SimpleMeshes.CreateCylinder(3, 3.5f, 5)
                        }
                    },
                }
            };
        }

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = (float4) ColorUint.White;

            _scene = CreateScene();

            //RC.SetRenderState(RenderState.CullMode, (uint) Cull.None, true);
            //RC.SetRenderState(RenderState.FillMode, (uint) FillMode.Wireframe, true);

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();

            for (int i = 0; i < 3; i++)
            {
                _baseTransform[i].Rotation = new float3(0, M.MinAngle(TimeSinceStart), 0);
            }

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            _camAngle += 45 * Time.DeltaTime * M.Pi * M.DegreesToRadians(45);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, 0, 40) * float4x4.CreateRotationX(-(float) Math.Atan(15.0 / 40.0)) * float4x4.CreateRotationZXY(new float3(M.DegreesToRadians(_camAngle),M.DegreesToRadians(_camAngle),M.DegreesToRadians(_camAngle)));

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
              
    }
}