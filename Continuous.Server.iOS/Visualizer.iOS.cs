using Xamarin.Forms;

namespace Continuous.Server
{
	public partial class Visualizer
	{
		bool presented = false;
		PreviewPage previewPage = new PreviewPage ();

		partial void PlatformStopVisualizing ()
		{
			Log ("Stop visualizing");
			Application.Current.MainPage.Navigation.PopModalAsync (false);
			presented = false;
		}

		partial void PlatformVisualize (EvalResult res)
		{
			var val = res.Result;
			var ty = val != null ? val.GetType () : typeof (object);
			Log ("{0} value = {1}", ty.FullName, val);

			Log ("Start visualizing");
			Page page = res.Result as Page;
			if (page == null && res.Result is View view) {
				page = new ContentPage () { Content = view };
				if (view.HeightRequest < 1 || view.WidthRequest < 1) {
					view.HorizontalOptions = LayoutOptions.Center;
					view.HorizontalOptions = LayoutOptions.Center;
				}
			}
			if (page != null) {
				if (!presented) {
					Application.Current.MainPage.Navigation.PushModalAsync (previewPage, false);
					presented = true;
				}
				previewPage.ChangePage (page);
			}
		}

		partial void PlatformInitialize ()
		{
		}
	}

	class PreviewPage : MultiPage<Page>
	{

		public void ChangePage (Page page)
		{
			this.Children.Clear ();
			this.Children.Add (page);
			CurrentPage = page;
		}

		protected override Page CreateDefault (object item)
		{
			return null;
		}
	}
}

