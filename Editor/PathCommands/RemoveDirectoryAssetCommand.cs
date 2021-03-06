﻿using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;
using UnityEngine;

namespace UniModules.UniBuild.Commands.Editor.PathCommands
{
    [CreateAssetMenu(menuName = "UniGame/UniBuild/Path/RemoveDirectory",fileName = nameof(RemoveDirectoryAssetCommand))]
    public class RemoveDirectoryAssetCommand : UnityPreBuildCommand
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.HideLabel]
        [Sirenix.OdinInspector.InlineProperty]
#endif
        public RemoveDirectoryCommand command = new RemoveDirectoryCommand();
        
        public override void Execute(IUniBuilderConfiguration configuration)
        {
            command.Execute();
        }
    }
}
