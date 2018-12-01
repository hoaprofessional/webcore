using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebCore.Utils.TreeViewHelper
{
    public static class TreeViewBuilder
    {
        private static T ShallowCopy<T>(T item) where T : class
        {
            MethodInfo dynMethod = item.GetType().GetMethod("MemberwiseClone",
    BindingFlags.NonPublic | BindingFlags.Instance);
            T obj = (T)dynMethod.Invoke(item, new object[0]);
            return obj;
        }


        public static T ToTreeView<T>(this List<T> collections, object rootNodeKey) where T : class, ITreeViewModel
        {
            if (collections == null || collections.Count == 0)
            {
                return null;
            }
            T rootNode = ShallowCopy(collections.First());
            rootNode.Key = rootNodeKey;
            Dictionary<object, ITreeViewModel> treeViewModelTable = new Dictionary<object, ITreeViewModel>();
            foreach (T item in collections)
            {
                item.Childs = new List<ITreeViewModel>();
                treeViewModelTable[item.Key] = item;
            }

            foreach (T item in collections)
            {
                if (treeViewModelTable.Keys.Contains(item.ParentKey))
                {
                    ITreeViewModel parent = treeViewModelTable[item.ParentKey];
                    item.Parent = parent;
                    parent.Childs.Add(item);
                }
            }

            rootNode.Childs = collections.Where(x => x.Parent == null).Select(x => (ITreeViewModel)x).ToList();
            foreach (ITreeViewModel child in rootNode.Childs)
            {
                child.Parent = rootNode;
            }

            return rootNode;
        }
    }
}
