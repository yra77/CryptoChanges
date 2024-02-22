

using LocalizationResourceManager.Maui;
using Microsoft.Extensions.Logging;


namespace CryptoChanges
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UsePrism(PrismStartup.Configure)
                .UseLocalizationResourceManager(settings =>
                {
                    settings.RestoreLatestCulture(true);
                    settings.AddResource(Resources.Strings.Resource.ResourceManager);
                })
                .ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("Background", (handler, view) =>
            {
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.Layer.BorderWidth = 0;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;

				//disable entry focus rect
				UIKit.UIFocusEffect uIFocus = UIKit.UIFocusEffect.Create();
				uIFocus.Dispose();
				handler.PlatformView.FocusEffect = uIFocus;

                //	handler.PlatformView.TintColor = UIKit.UIColor.Green; //cursor
            });

			builder.Services.AddLocalization();
            //builder.Services.AddLocalization(options =>
            //{
            //    options.ResourcesPath = "CryptoChanges/Resources/Strings";
            //});
            builder.ConfigureAnimations();

            return builder.Build();
		}
	}
}