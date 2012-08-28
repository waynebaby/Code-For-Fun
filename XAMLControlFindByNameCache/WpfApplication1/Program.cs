using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{

    public class AllControlNameCache
    {
        static AllControlNameCache()
        {

        }

        static Dictionary<string, LinkedList<WeakReference<FrameworkElement>>> controlsCache
            = new Dictionary<string, LinkedList<WeakReference<FrameworkElement>>>();
        public static void InitCache()
        {
            var nmeta = FrameworkElement.NameProperty.GetMetadata(DependencyObjectType.FromSystemType(typeof(FrameworkElement)));
            var fi = typeof(PropertyMetadata).GetField("_propertyChangedCallback", BindingFlags.NonPublic | BindingFlags.Instance);
            var newcb = new PropertyChangedCallback((o, e) =>
            {
                if (!string.IsNullOrEmpty((string)e.NewValue))
                {


                    //MessageBox.Show(string.Format("object:{0},\r\nname:{1}", o.ToString(), e.NewValue.ToString()));
                    LinkedList<WeakReference<FrameworkElement>> targetList;
                    var key = e.NewValue.ToString();
                    targetList = GetListByKey(key);
                    targetList.AddLast(new WeakReference<FrameworkElement>(o as FrameworkElement));
                }
            });
            fi.SetValue(nmeta, newcb);

        }

        private static LinkedList<WeakReference<FrameworkElement>> GetListByKey(string key)
        {
            LinkedList<WeakReference<FrameworkElement>> targetList;
            if (!controlsCache.TryGetValue(key, out targetList))
            {
                targetList = new LinkedList<WeakReference<FrameworkElement>>();
                controlsCache[key] = targetList;
            }
            return targetList;
        }

        public static IEnumerable<FrameworkElement> GetControls(string name)
        {
            var lst = GetListByKey(name);
            var node = lst.First;
            while (node != null)
            {
                var next = node.Next;
                FrameworkElement target;

                if (node.Value.TryGetTarget(out target))
                {
                    yield return target;
                }
                else
                {
                    lst.Remove(node);
                }
                node = next;
            }

        }
    }

    class Program
    {
        [System.STAThreadAttribute()]

        public static void Main()
        {
            AllControlNameCache.InitCache();
            WpfApplication1.App app = new WpfApplication1.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
