namespace Nabeey.Service.Extensions;

public static class YouTubeLinkValidator
{
	public static bool IsValidYouTubeLink(string youtubeLink)
	{
		if (string.IsNullOrWhiteSpace(youtubeLink))
		{
			return false;
		}
		try
		{
			Uri uri = new Uri(youtubeLink);

			if (uri.Host.Equals("www.youtube.com", StringComparison.OrdinalIgnoreCase) ||
				uri.Host.Equals("youtu.be", StringComparison.OrdinalIgnoreCase))
			{
				if (uri.AbsolutePath.StartsWith("/watch", StringComparison.OrdinalIgnoreCase) ||
					uri.AbsolutePath.StartsWith("/", StringComparison.OrdinalIgnoreCase))
				{
					string queryString = uri.Query;
					if (!string.IsNullOrWhiteSpace(queryString))
					{
						string[] queryParameters = queryString.TrimStart('?').Split('&');
						foreach (string parameter in queryParameters)
						{
							string[] keyValue = parameter.Split('=');
							if (keyValue.Length == 2 && keyValue[0].Equals("v", StringComparison.OrdinalIgnoreCase))
							{
								string videoId = keyValue[1];
								if (!string.IsNullOrWhiteSpace(videoId))
								{
									return true;
								}
							}
						}
					}
				}
			}
		}
		catch (UriFormatException)
		{
			return false;
		}
		return false;
	}
}