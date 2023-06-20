using ARKit;
using SceneKit;
using System;
using System.Linq;
using UIKit;

namespace StepTrackar
{
    public partial class StepTrackarViewController : UIViewController
    {
        private readonly ARSCNView sceneView;

        TerrainNode terrainNode;
        YearlyChartNode stepHistoryNode;
        ForestNode forestNode;

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


            // Add static images
            float z = -0.5f;

            // Details
            var logoNode = new StepTrackarImageNode(0.2, "step-trackar.png", 0.3f, 0.07f, new SCNVector3(0.2f, 0.35f, z));
            var profileNode = new StepTrackarImageNode(0.4, "profile-details.png", 0.1f, 0.1f, new SCNVector3(-0.3f, 0.22f, z));
            var dateTimeNode = new StepTrackarImageNode(0.6, "date-and-time.png", 0.1f, 0.1f, new SCNVector3(-0.3f, 0.1f, z));

            //Steps
            var personalStepsNode = new StepTrackarImageNode(0.8, "personal-steps.png", 0.15f, 0.04f, new SCNVector3(-0.12f, 0.25f, z));
            var personalSteps1Node = new StepTrackarImageNode(1.0, "personal-steps-1.png", 0.05f, 0.05f, new SCNVector3(-0.18f, 0.2f, z));
            var personalSteps2Node = new StepTrackarImageNode(1.2, "personal-steps-2.png", 0.05f, 0.05f, new SCNVector3(-0.18f + 0.06f, 0.2f, z));
            var personalSteps3Node = new StepTrackarImageNode(1.4, "personal-steps-3.png", 0.05f, 0.05f, new SCNVector3(-0.18f + 0.12f, 0.2f, z));

            var teamStepsNode = new StepTrackarImageNode(1.6, "london-team-steps.png", 0.2f, 0.04f, new SCNVector3(-0.12f, 0.1f, z));
            var teamSteps1Node = new StepTrackarImageNode(1.8, "team-steps-1.png", 0.05f, 0.05f, new SCNVector3(-0.18f, 0.05f, z));
            var teamSteps2Node = new StepTrackarImageNode(1.9, "team-steps-2.png", 0.05f, 0.05f, new SCNVector3(-0.18f + 0.06f, 0.05f, z));
            var teamSteps3Node = new StepTrackarImageNode(2, "team-steps-3.png", 0.05f, 0.05f, new SCNVector3(-0.18f + 0.12f, 0.05f, z));

            // Map
            var personalWalksNode = new StepTrackarImageNode(2.2, "personal-walks.png", 0.15f, 0.04f, new SCNVector3(0.1f, 0.25f, z));
            var map1Node = new StepTrackarImageNode(2.4, "map-1.png", 0.1f, 0.2f, new SCNVector3(0.05f, 0.1f, z));

            var tapGesture = new UITapGestureRecognizer(HandleTapGesture);
            this.sceneView.AddGestureRecognizer(tapGesture);


            var map2Node = new StepTrackarImageNode(2.6, "map-2.png", 0.1f, 0.2f, new SCNVector3(0.18f, 0.1f, z));

            // Trees
            var personalTreesNode = new StepTrackarImageNode(2.8, "personal-trees.png", 0.1f, 0.04f, new SCNVector3(0.30f, 0.25f, z));
            var treeCount1Node = new StepTrackarImageNode(3, "tree-count-1.png", 0.1f, 0.05f, new SCNVector3(0.3f, 0.2f, z));

            var teamTreesNode = new StepTrackarImageNode(3.2, "team-trees.png", 0.1f, 0.04f, new SCNVector3(0.3f, 0.1f, z));
            var treeCount2Node = new StepTrackarImageNode(3.4, "tree-count-2.png", 0.1f, 0.05f, new SCNVector3(0.3f, 0.05f, z));

            // Leaderboard
            var monthlyLeaderboardNode = new StepTrackarImageNode(3.6, "monthly-leaderboard.png", 0.3f, 0.05f, new SCNVector3(0.55f, 0.25f, z));
            var leaderboardNode = new StepTrackarImageNode(3.8, "leaderboard.png", 0.4f, 0.25f, new SCNVector3(0.60f, 0.1f, z));

            // Step history
            stepHistoryNode = new YearlyChartNode();
            stepHistoryNode.Opacity = 0.95f;
            stepHistoryNode.Position = new SCNVector3(-0.4f, -0.35f, -0.5f);

            // Terrain
            terrainNode = new TerrainNode();
            terrainNode.Opacity = 0;
            terrainNode.Position = new SCNVector3(0.2f, -0.1f, -0.4f);

            forestNode = new ForestNode();
            forestNode.Opacity = 0;
            forestNode.Scale = new SCNVector3(0.1f, 0.1f, 0.1f);
            forestNode.Position = new SCNVector3(-0.6f, -0.1f, -0.5f);



            this.sceneView.Scene.RootNode.AddChildNode(logoNode);
            this.sceneView.Scene.RootNode.AddChildNode(profileNode);
            this.sceneView.Scene.RootNode.AddChildNode(dateTimeNode);

            this.sceneView.Scene.RootNode.AddChildNode(personalStepsNode);
            this.sceneView.Scene.RootNode.AddChildNode(teamStepsNode);

            this.sceneView.Scene.RootNode.AddChildNode(personalSteps1Node);
            this.sceneView.Scene.RootNode.AddChildNode(personalSteps2Node);
            this.sceneView.Scene.RootNode.AddChildNode(personalSteps3Node);
            this.sceneView.Scene.RootNode.AddChildNode(teamSteps1Node);
            this.sceneView.Scene.RootNode.AddChildNode(teamSteps2Node);
            this.sceneView.Scene.RootNode.AddChildNode(teamSteps3Node);



            this.sceneView.Scene.RootNode.AddChildNode(personalWalksNode);
            this.sceneView.Scene.RootNode.AddChildNode(map1Node);
            this.sceneView.Scene.RootNode.AddChildNode(map2Node);

            this.sceneView.Scene.RootNode.AddChildNode(personalTreesNode);
            this.sceneView.Scene.RootNode.AddChildNode(treeCount1Node);
            this.sceneView.Scene.RootNode.AddChildNode(teamTreesNode);
            this.sceneView.Scene.RootNode.AddChildNode(treeCount2Node);

            this.sceneView.Scene.RootNode.AddChildNode(monthlyLeaderboardNode);
            this.sceneView.Scene.RootNode.AddChildNode(leaderboardNode);

            this.sceneView.Scene.RootNode.AddChildNode(stepHistoryNode);
            this.sceneView.Scene.RootNode.AddChildNode(terrainNode);
            this.sceneView.Scene.RootNode.AddChildNode(forestNode);





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


            if (node.Name.Contains("map"))
            {
                forestNode.Opacity = 0;

                // Hide steps
                stepHistoryNode.Opacity = 0;

                // Show map
                terrainNode.Show();
            }
            else if (node.Name.Contains("steps"))
            {
                forestNode.Opacity = 0;

                // Hide map
                terrainNode.Opacity = 0;

                // Show steps
                stepHistoryNode.Show();
            }
            else if (node.Name.Contains("tree"))
            {
                // Hide map
                terrainNode.Opacity = 0;

                // Show steps
                stepHistoryNode.Opacity = 0;

                forestNode.Show();
            }

        }

        public class ForestNode : SCNNode
        {
            SCNAction fadeInAction;

            public ForestNode()
            {
                fadeInAction = SCNAction.FadeOpacityTo(0.99f, 2);

                int row = 1;
                int col = 1;

                for (int day = 1; day < 100; day++)
                {
                    {
                        var tempTreeNode = new TreeNode();
                        tempTreeNode.Position = new SCNVector3(col * 0.6f, -0.05f, row * 0.6f); ;
                        this.AddChildNode(tempTreeNode);

                        row++;

                        if (row == 5)
                        {
                            col++;
                            row = 1;
                        }
                    }
                }
            }

            public void Show()
            {
                RunAction(fadeInAction);
            }
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            this.sceneView.Session.Pause();
        }

        public class YearlyChartNode : SCNNode
        {
            SCNAction fadeInAction;

            public YearlyChartNode()
            {
                fadeInAction = SCNAction.FadeOpacityTo(0.9f, 3);

                int row = 1;
                int column = 1;

                var random = new Random();

                for (int x = 0; x < 365; x++)
                {
                    int steps = random.Next(500, 15000);

                    var dayNode = new DayNode(column, row, steps);

                    row++;

                    if (row == 7)
                    {
                        // Move to the next column and back to first row (new week)
                        row = 0;
                        column++;
                    }

                    this.EulerAngles = new SCNVector3((float)-Math.PI / 2, 0, 0);
                    this.AddChildNode(dayNode);
                }
            }

            public void Show()
            {
                this.RunAction(fadeInAction);
            }
        }

        public class DayNode : SCNNode
        {
            public int Column { get; set; }
            public int Row { get; set; }
            public int Steps { get; set; }
            public UIColor color { get; set; }

            SCNMaterial originalMaterial;
            SCNVector3 originalPosition;
            float Margin = 0.0025f;
            float WidthAndHeight = 0.02f;



            public DayNode(int column, int row, int steps)
            {
                this.Column = column;
                this.Row = row;
                this.Steps = steps;
                this.Opacity = 0;

                if (Steps >= 5000)
                {
                    color = UIColor.FromRGB(68, 163, 64);
                }
                else
                {
                    color = UIColor.Red;
                }

                float endZPosition = 0.08f;

                SCNAction delay = SCNAction.Wait(4.2 + (Column * 0.1));
                SCNAction fadeIn = SCNAction.FadeOpacityTo(0.95f, 0.15);
                SCNAction moveIn = SCNAction.MoveBy(new SCNVector3(0, 0, endZPosition), 0.2);
                SCNAction fadeInAndMove = SCNAction.Group(new[] { fadeIn, moveIn });

                SCNAction delayAndFadeInAndMove = SCNAction.Sequence(new[] { delay, fadeInAndMove });
                this.RunAction(delayAndFadeInAndMove);

                SCNMaterial material = new SCNMaterial();
                material.DoubleSided = true;
                material.Diffuse.Contents = color;



                var height = (float)((float)steps / (float)100000);
                this.Geometry = SCNBox.Create(WidthAndHeight, WidthAndHeight, height, 0);

                this.Geometry.FirstMaterial = material;

                originalMaterial = material;

                // Change z position based on height
                var z = endZPosition + (height / 2);

                originalPosition = new SCNVector3(
                    (column * WidthAndHeight) + (column * Margin), // X
                    (-row * WidthAndHeight) + (-row * Margin), // Y
                    z);

                Position = originalPosition;

            }
        }

        public class TerrainNode : SCNNode
        {
            SCNAction fadeInAction;

            public TerrainNode()
            {
                fadeInAction = SCNAction.FadeOpacityTo(1f, 2);

                this.AddChildNode(CreateModelNodeFromFile("art.scnassets/mam-tor-1-2.dae"));
            }

            public SCNNode CreateModelNodeFromFile(string filePath)
            {
                var sceneFromFile = SCNScene.FromFile(filePath);

                var model = sceneFromFile.RootNode.FindChildNode("EXPORT_GOOGLE_SAT_WM", true);
                model.Scale = new SCNVector3(0.1f, 0.1f, 0.1f);
                model.Position = new SCNVector3(0, -0.2f, 0);

                return model;
            }

            public void Show()
            {
                this.RunAction(fadeInAction);
            }
        }

        public class StepTrackarImageNode : SCNNode
        {

            public StepTrackarImageNode(double animationWaitDuration, string imagePath, float width, float height, SCNVector3 position)
            {
                Geometry = CreateGeometry("StepTrackar/" + imagePath, width, height);
                Position = position;
                Opacity = 0;

                this.Name = imagePath;

                // Animations
                var endPosition = position;
                var startPosition = endPosition;
                startPosition.Z = endPosition.Z - 0.2f;

                SCNAction moveCloserAction = SCNAction.MoveTo(endPosition, 0.3);
                SCNAction waitAction = SCNAction.Wait(animationWaitDuration);
                SCNAction fadeInAction = SCNAction.FadeOpacityTo(0.95f, 0.3);
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