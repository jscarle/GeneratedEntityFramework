using DiffEngine;
using VerifyTests.DiffPlex;

namespace GeneratedEntityFramework.Tests;

public static class ModuleInitializer
{
    private static readonly object Lock = new();
    private static bool _isInitialized;

    public static void Initialize()
    {
        lock (Lock)
        {
            if (_isInitialized)
                return;

            DiffRunner.Disabled = true;
            VerifyDiffPlex.Initialize(OutputType.Compact);
            VerifierSettings.InitializePlugins();
            VerifierSettings.ScrubLinesContaining("DiffEngineTray");
            VerifierSettings.IgnoreStackTrace();
            //VerifierSettings.AutoVerify();
            _isInitialized = true;
        }
    }
}
