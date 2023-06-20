using ARKit;
using SceneKit;
using System;
using System.Linq;
using UIKit;

namespace StepTrackAR
{
    public partial class StepTrackarViewController : UIViewController
    {
        private readonly ARSCNView sceneView;

        public StepTrackarViewController()
        {
            this.sceneView = new ARSCNView
            {
                AutoenablesDefaultLighting = true
            };

            this.View.AddSubview(this.sceneView);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.sceneView.Frame = this.View.Frame;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            this.sceneView.Session.Run(new ARWorldTrackingConfiguration
            {
                AutoFocusEnabled = true,
                PlaneDetection = ARPlaneDetection.Horizontal,
                LightEstimationEnabled = true,
                WorldAlignment = ARWorldAlignment.Gravity
            }, ARSessionRunOptions.ResetTracking | ARSessionRunOptions.RemoveExistingAnchors);

            //var size = 1f;


            //var metricNode = new SCNNode();

            // Add static images
            float z = -0.5f;

            // Details
            var logoNode = new StepTrackarImageNode(0.2, "step-trackar.png", 0.25f, 0.07f, new SCNVector3(0, 0.35f, z));
            var profileNode = new StepTrackarImageNode(0.4, "profile-details.png", 0.1f, 0.1f, new SCNVector3(-0.3f, 0.22f, z));
            var dateTimeNode = new StepTrackarImageNode(0.6, "date-and-time.png", 0.1f, 0.1f, new SCNVector3(-0.3f, 0.1f, z));

            //Steps
            var personalStepsNode = new StepTrackarImageNode(0.8, "personal-steps.png", 0.15f, 0.05f, new SCNVector3(-0.12f, 0.25f, z));
            var teamStepsNode = new StepTrackarImageNode(0.9, "london-team-steps.png", 0.2f, 0.05f, new SCNVector3(-0.1f, 0.1f, z));

            // Map
            var personalWalksNode = new StepTrackarImageNode(1, "personal-walks.png", 0.15f, 0.04f, new SCNVector3(0.1f, 0.25f, z));
            var map1Node = new StepTrackarImageNode(1.2, "map-1.png", 0.1f, 0.2f, new SCNVector3(0.05f, -0.05f, z));
            var mapTapGesture = new UITapGestureRecognizer(HandleTapGesture);
            this.sceneView.AddGestureRecognizer(mapTapGesture);


            var map2Node = new StepTrackarImageNode(1.4, "map-2.png", 0.1f, 0.2f, new SCNVector3(0.18f, -0.05f, z));

            // Trees
            var personalTreesNode = new StepTrackarImageNode(1.6, "personal-trees.png", 0.1f, 0.05f, new SCNVector3(0.30f, 0.27f, z));
            var treeCount1Node = new StepTrackarImageNode(1.8, "tree-count-1.png", 0.1f, 0.05f, new SCNVector3(0.3f, 0.2f, z));

            var teamTreesNode = new StepTrackarImageNode(2, "team-trees.png", 0.1f, 0.05f, new SCNVector3(0.3f, 0.1f, z));
            var treeCount2Node = new StepTrackarImageNode(2.2, "tree-count-2.png", 0.1f, 0.05f, new SCNVector3(0.3f, -0.2f, z));

            // Leaderboard
            var monthlyLeaderboardNode = new StepTrackarImageNode(2.4, "monthly-leaderboard.png", 0.1f, 0.05f, new SCNVector3(0.5f, 0.27f, z));
            var leaderboardNode = new StepTrackarImageNode(2.6, "leaderboard.png", 0.1f, 0.05f, new SCNVector3(0.5f, -0.2f, z));


            this.sceneView.Scene.RootNode.AddChildNode(logoNode);
            this.sceneView.Scene.RootNode.AddChildNode(profileNode);
            this.sceneView.Scene.RootNode.AddChildNode(dateTimeNode);

            this.sceneView.Scene.RootNode.AddChildNode(personalStepsNode);
            this.sceneView.Scene.RootNode.AddChildNode(teamStepsNode);

            this.sceneView.Scene.RootNode.AddChildNode(personalWalksNode);
            this.sceneView.Scene.RootNode.AddChildNode(map1Node);
            this.sceneView.Scene.RootNode.AddChildNode(map2Node);

            this.sceneView.Scene.RootNode.AddChildNode(personalTreesNode);
            this.sceneView.Scene.RootNode.AddChildNode(treeCount1Node);
            this.sceneView.Scene.RootNode.AddChildNode(teamTreesNode);
            this.sceneView.Scene.RootNode.AddChildNode(treeCount2Node);

            this.sceneView.Scene.RootNode.AddChildNode(monthlyLeaderboardNode);
            this.sceneView.Scene.RootNode.AddChildNode(leaderboardNode);



            /*
            int row =1;
            int col=1;

        

            for(int day=1;day<365;day++)
            { 


                {
                    var tempTreeNode = new TreeNode();
                    tempTreeNode.Position = new SCNVector3(col*0.6f, -0.05f, row * 0.6f);;
                    this.sceneView.Scene.RootNode.AddChildNode(tempTreeNode);

                    row++;

                    if(row == 7)
                    {
                        col++;
                        row = 1;
                    }
                }
            }
            */

            //var treeNode = new TreeNode();

            //this.sceneView.Scene.RootNode.AddChildNode(treeNode);
        }

        private void HandleTapGesture(UITapGestureRecognizer sender)
        {
            var areaTapped = sender.View as SCNView;
            var location = sender.LocationInView(areaTapped);
            var hitTestResults = areaTapped.HitTest(location, new SCNHitTestOptions());

            var hitTest = hitTestResults.FirstOrDefault();

            if (hitTest == null)
                return;

            var node = hitTest.Node;

            // If map touch
            // Show map node (animations and all)

            // If is tree touch

            //var material = new SCNMaterial();
            //material.Diffuse.Contents = UIColor.Black;
            //node.Geometry.FirstMaterial = material;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            this.sceneView.Session.Pause();
        }

        public class LogoNode : SCNNode
        {
            public LogoNode()
            {

            }
        }

        public class MetricNode : SCNNode
        {
            public MetricNode()
            {

            }
        }

        public class YearlyChartNode : SCNNode
        {

        }

        public class DayNode : SCNNode
        {

        }

        public class TerrainNode : SCNNode
        {

        }

        public class StepTrackarImageNode : SCNNode
        {

            public StepTrackarImageNode(double animationWaitDuration, string imagePath, float width, float height, SCNVector3 position)
            {
                Geometry = CreateGeometry("Images/" + imagePath, width, height);
                Position = position;
                Opacity = 0;

                // Animations
                var endPosition = position;
                var startPosition = endPosition;
                startPosition.Z = endPosition.Z - 0.2f;

                //Position = startPosition;

                //SCNAction scaleUpAction = SCNAction.ScaleBy(2f, 0.3);
                SCNAction moveCloserAction = SCNAction.MoveTo(endPosition, 0.3);
                SCNAction waitAction = SCNAction.Wait(animationWaitDuration);
                SCNAction fadeInAction = SCNAction.FadeOpacityTo(0.9f, 0.3);
                SCNAction fadeInAndMoveAndScaleAction = SCNAction.Group(new[] { fadeInAction, moveCloserAction });
                SCNAction waitThenFadeAndMoveInAction = SCNAction.Sequence(new[] { waitAction, fadeInAndMoveAndScaleAction });

                this.RunAction(waitThenFadeAndMoveInAction);
            }

            private SCNGeometry CreateGeometry(string imagePath, float width, float height)
            {
                var material = new SCNMaterial();
                material.Diffuse.Contents = UIImage.FromFile(imagePath);
                material.DoubleSided = true;

                var geometry = SCNPlane.Create(width, height);
                geometry.Materials = new[] { material };

                return geometry;
            }
        }

        public class TreeNode : SCNNode
        {
            public TreeNode()
            {
                this.AddChildNode(CreateModelNodeFromFile("StepTrackar/Tree2.dae"));
            }

            public SCNNode CreateModelNodeFromFile(string filePath)
            {
                var sceneFromFile = SCNScene.FromFile(filePath);

                var model = sceneFromFile.RootNode.FindChildNode("Main", true);
                model.Scale = new SCNVector3(0.05f, 0.05f, 0.05f);
                model.Position = new SCNVector3(0, -0.2f, -0.5F);
                model.Rotation = new SCNVector4(1, 0, 0, ConvertDegreesToRadians(-90));

                return model;
            }

            public float ConvertDegreesToRadians(float angle)
            {
                return (float)(Math.PI / 180) * angle;
            }
        }

    }
}
