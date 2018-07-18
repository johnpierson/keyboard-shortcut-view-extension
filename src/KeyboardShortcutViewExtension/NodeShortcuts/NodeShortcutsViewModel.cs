using System;
using Dynamo.Core;
using Dynamo.Extensions;
using Dynamo.Graph.Nodes;

namespace KeyboardShortcutViewExtension
{
    public class NodeShortcutsViewModel : NotificationObject, IDisposable
    {
        private ReadyParams readyParams;


        public NodeShortcutsViewModel(ReadyParams p)
        {
            readyParams = p;
            p.CurrentWorkspaceModel.NodeAdded += CurrentWorkspaceModel_NodesChanged;
            p.CurrentWorkspaceModel.NodeRemoved += CurrentWorkspaceModel_NodesChanged;
        }

        private void CurrentWorkspaceModel_NodesChanged(NodeModel obj)
        {
            RaisePropertyChanged();
        }

        public void Dispose()
        {
        }
    }
}
