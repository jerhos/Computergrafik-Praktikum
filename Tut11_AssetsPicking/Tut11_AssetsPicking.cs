using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Engine.Core.Effects;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut11_AssetsPicking", Description = "Yet another FUSEE App.")]
    public class Tut11_AssetsPicking : RenderCanvas
    {
        private SceneContainer _scene;
        private ScenePicker _scenePicker;
        private SceneRendererForward _sceneRenderer;
        private PickResult _currentPick;
        private float4 _oldColor;
        
        
        //---Transforms---\\
        // Body (Wall-E)
        private Transform _baseTransform;
        // Wheels
        private Transform _rightRearTransform;
        private Transform _rightFrontTransform;
        private Transform _rightUpperTransform;
        private Transform _leftRearTransform;
        private Transform _leftFrontTransform;
        private Transform _leftUpperTransform;
        // Neck
        private Transform _lowerNeck;
        private Transform _upperNeck;
        // Head
        private Transform _head;

        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            InitializeTransform(_baseTransform      );
            InitializeTransform(_rightRearTransform );
            InitializeTransform(_rightFrontTransform);
            InitializeTransform(_rightUpperTransform);
            InitializeTransform(_leftRearTransform  );
            InitializeTransform(_leftFrontTransform );
            InitializeTransform(_leftUpperTransform );
            InitializeTransform(_lowerNeck          );
            InitializeTransform(_upperNeck          );
            InitializeTransform(_head               );

            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNode>
                { 
                }
            };
        }

        // Init is called on startup. 
        public override void Init()
        {
            RC.ClearColor = new float4(0, 0, 0.1f, 1);

            _scene = AssetStorage.Get<SceneContainer>("WALL-E_aufWishBestellt.fus"); // Bei meinem ursprünglichen Modell (WALL-E.fus) wird die Farbe anstatt des Objektes ausgewählt
            _scenePicker = new ScenePicker(_scene);

            _baseTransform          = GetTransformOf("Wall-E");
            _rightRearTransform     = GetTransformOf("rightRearWheel");
            _rightFrontTransform    = GetTransformOf("rightFrontWheel");
            _rightUpperTransform    = GetTransformOf("rightUpperWheel");
            _leftRearTransform      = GetTransformOf("leftRearWheel");
            _leftFrontTransform     = GetTransformOf("leftFrontWheel");
            _leftUpperTransform     = GetTransformOf("leftUpperWheel");
            _lowerNeck              = GetTransformOf("neck1");
            _upperNeck              = GetTransformOf("neck2");
            _head                   = GetTransformOf("head");

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        public override async Task InitAsync()
        {
            await base.InitAsync();
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();

            _baseTransform.Rotation = new float3(0, M.MinAngle(TimeSinceStart), 0);

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);
            
            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, 0, 40) * float4x4.CreateRotationX(-(float) Math.Atan(15.0 / 40.0));

            
            if (Mouse.LeftButton)
            {
                float2 pickPosClip = Mouse.Position * new float2(2.0f / Width, -2.0f / Height) + new float2(-1, 1);
          
                PickResult newPick = _scenePicker.Pick(RC, pickPosClip).OrderBy(pr => pr.ClipPos.z).FirstOrDefault();
          
                 if (newPick?.Node != _currentPick?.Node)
                 {
                     if (_currentPick != null)
                     {
                         var ef = _currentPick.Node.GetComponent<SurfaceEffect>();
                         ef.SurfaceInput.Albedo = _oldColor;
                     }
                     if (newPick != null)
                     {
                         var ef = newPick.Node.GetComponent<SurfaceEffect>();
                         _oldColor = ef.SurfaceInput.Albedo;
                         ef.SurfaceInput.Albedo = (float4) ColorUint.White;
                         Diagnostics.Debug($"Object {newPick.Node.Name} picked.");
                     }
                     _currentPick = newPick;
                  }
            }
            if (_currentPick != null)
            {
            setRotationOfCurrent();
            }
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

        public Transform GetTransformOf (string name) 
        {
            return _scene.Children.FindNodes(node => node.Name == name)?.FirstOrDefault()?.GetTransform();
        }   

        public void InitializeTransform (Transform t) 
        {
            t = new Transform
            {
                Rotation    = new float3(0, 0, 0),
                Scale       = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };
        }

        public void setRotationOfCurrent()
        {
                float speed = 0.1f;
                switch (_currentPick.Node.Name)
                {
                    case "Wall-E":
                        _currentPick.Node.GetTransform().Rotation += new float3(-Keyboard.UpDownAxis * (speed * 10), Keyboard.ADAxis * (speed * 20), Keyboard.LeftRightAxis * (speed * 10));
                        Diagnostics.Debug(_currentPick.Node.GetTransform().Rotation);
                    break;
                    case "rightRearWheel":
                        _currentPick.Node.GetTransform().Rotation += new float3(-Keyboard.UpDownAxis * speed, 0, 0);
                        Diagnostics.Debug(_currentPick.Node.GetTransform().Rotation);
                    break;
                    case "rightFrontWheel":
                        _currentPick.Node.GetTransform().Rotation += new float3(-Keyboard.UpDownAxis * speed, 0, 0);
                        Diagnostics.Debug(_currentPick.Node.GetTransform().Rotation);
                    break;
                    case "rightUpperWheel":
                        _currentPick.Node.GetTransform().Rotation += new float3(-Keyboard.UpDownAxis * speed, 0, 0);
                        Diagnostics.Debug(_currentPick.Node.GetTransform().Rotation);
                    break;
                    case "leftRearWheel":
                        _currentPick.Node.GetTransform().Rotation += new float3(-Keyboard.UpDownAxis * speed, 0, 0);
                        Diagnostics.Debug(_currentPick.Node.GetTransform().Rotation);
                    break;
                    case "leftFrontWheel":
                        _currentPick.Node.GetTransform().Rotation += new float3(-Keyboard.UpDownAxis * speed, 0, 0);
                        Diagnostics.Debug(_currentPick.Node.GetTransform().Rotation);
                    break;
                    case "leftUpperWheel":
                        _currentPick.Node.GetTransform().Rotation += new float3(-Keyboard.UpDownAxis * speed, 0, 0);
                        Diagnostics.Debug(_currentPick.Node.GetTransform().Rotation);
                    break;
                    case "neck1":
                        _currentPick.Node.Parent.GetTransform().Rotation += new float3(-Keyboard.UpDownAxis * speed, Keyboard.ADAxis * speed, Keyboard.LeftRightAxis * speed);
                        Diagnostics.Debug(_currentPick.Node.Parent.GetTransform().Rotation);
                    break;
                    case "neck2":
                        _currentPick.Node.Parent.GetTransform().Rotation += new float3(-Keyboard.UpDownAxis * speed, Keyboard.ADAxis * speed, Keyboard.LeftRightAxis * speed);
                        Diagnostics.Debug(_currentPick.Node.Parent.GetTransform().Rotation);
                    break;
                    case "head":
                        _currentPick.Node.GetTransform().Rotation += new float3(0, Keyboard.ADAxis * speed, 0);
                        Diagnostics.Debug(_currentPick.Node.GetTransform().Rotation);
                    break;
                    default:
                        Diagnostics.Debug("Found no scenenode as current pick");
                    break;
            }
        }        
    }
}