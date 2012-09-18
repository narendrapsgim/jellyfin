﻿using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using System.ComponentModel.Composition;

namespace MediaBrowser.Controller.Resolvers
{
    [Export(typeof(IBaseItemResolver))]
    public class FolderResolver : BaseFolderResolver<Folder>
    {
        public override ResolverPriority Priority
        {
            get { return ResolverPriority.Last; }
        }
        
        protected override Folder Resolve(ItemResolveEventArgs args)
        {
            if (args.IsDirectory && !args.IsMetadataFolder)
            {
                return new Folder() { PhysicalLocations = args.PhysicalLocations };
            }

            return null;
        }
    }

    public abstract class BaseFolderResolver<TItemType> : BaseItemResolver<TItemType>
        where TItemType : Folder, new()
    {
        protected override void SetInitialItemValues(TItemType item, ItemResolveEventArgs args)
        {
            base.SetInitialItemValues(item, args);

            item.IsRoot = args.Parent == null;
        }
    }
}
