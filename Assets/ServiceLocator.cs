public static class ServiceLocator
{
    private static IAudioService audioService;

    public static IAudioService GetAudioProvider()
    {
        return audioService;
    }

    public static void SetAudioProvider(IAudioService newService)
    {
        if (audioService != null)
        {
            audioService.Unitialize();
        }

        audioService = newService;
        audioService.Initialize();
    }
}