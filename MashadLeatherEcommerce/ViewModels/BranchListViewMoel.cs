using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class BranchListViewMoel:_BaseViewModel
    {
        public List<BranchItemViewModel> Branches { get; set; }
    }

    public class BranchItemViewModel
    {
        public SiteBranchGroup BranchGroup { get; set; }
        public List<SiteBranch> SiteBranches { get; set; }

    }
}