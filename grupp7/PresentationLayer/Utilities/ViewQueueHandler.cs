using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationLayer.ViewModels;

namespace PresentationLayer.Utilities
{
    // This class handles storage of recent views. Used for going back to last view by pressing a button.
    public class ViewQueueHandler
    {
        public List<BaseViewModel> recentViews;
        public ViewQueueHandler()
        {
            //Stores recent views. Max 10
            recentViews = new List<BaseViewModel>();
        }

        //Going to a new view
        public void NewView(BaseViewModel oldView)
        {
            //Put newView in front of list and push back old views
            recentViews = recentViews.Prepend(oldView).ToList();
        }

        public bool IsEmpty()
        {
            return recentViews.Count == 0;
        }

        //Going back to recent view
        public BaseViewModel GoBack()
        {
            if(recentViews.Count != 0)
            {
                BaseViewModel returnView = recentViews.ElementAt(0);
                recentViews.RemoveAt(0);
                return returnView;
            }

            return null;
        }
    }
}
