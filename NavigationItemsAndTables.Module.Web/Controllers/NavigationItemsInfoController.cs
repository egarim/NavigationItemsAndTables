using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace NavigationItemsAndTables.Module.Web.Controllers
{
    public class Node
    {
        public string Name { get; set; }
        public Node ParentNode { get; set; }
        public string ParentNodeName { get; set; }
        public string Table { get; set; }
        public Node()
        {
            
        }
    }
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class NavigationItemsInfoController : ViewController
    {
        SimpleAction GenerateNavigationInformation;
        Session currentSession;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public NavigationItemsInfoController()
        {
            InitializeComponent();
            GenerateNavigationInformation = new SimpleAction(this, "Generate Navigation Information", "View");
            GenerateNavigationInformation.Execute += GenerateNavigationInformation_Execute;
            
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void GenerateNavigationInformation_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            List<Node> nodes = new List<Node>();
            currentSession = (this.ObjectSpace as XPObjectSpace).Session;
            IModelRootNavigationItems NavigationItems = this.Application.Model.GetNode("NavigationItems") as IModelRootNavigationItems;
            foreach (IModelNavigationItem modelNavigationItem in NavigationItems.Items)
            {
                Node newNode = new Node();
                nodes.Add(newNode);
                newNode.Name = modelNavigationItem.Caption;

                IModelView test = modelNavigationItem.View;
                GetSubNodes(modelNavigationItem, newNode, nodes);

            }
            string json = JsonConvert.SerializeObject(nodes, Formatting.Indented,new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }

        private void GetSubNodes(IModelNavigationItem modelNavigationItem, Node newNode, List<Node> nodes)
        {
            if (modelNavigationItem.Items.Count > 0)
            {
                foreach (IModelNavigationItem SubNavigationItem in modelNavigationItem.Items)
                {
                    Node SubNode = new Node();
                    SubNode.Name = SubNavigationItem.Caption;
                    SubNode.ParentNode = newNode;
                    SubNode.ParentNodeName = newNode.Name;
                    if (SubNavigationItem.View!=null)
                    {
                        //DevExpress.ExpressApp.Model.I test;
                        IModelListView modelListView = (SubNavigationItem.View as IModelListView);
                        if (modelListView == null)
                            continue;

                        IModelClass modelClass = modelListView.ModelClass;
                        var TypeInfo= this.Application.TypesInfo.FindTypeInfo(modelClass.Name);
                        SubNode.Table = this.currentSession.GetClassInfo(TypeInfo.Type).Table.Name;
                        
                     
                    }
                    nodes.Add(SubNode);
                    this.GetSubNodes(SubNavigationItem, SubNode, nodes);
                }
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
