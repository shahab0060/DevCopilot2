namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class SeoExtensions
    {

        //public static List<string> GetImageLocations(
        //   this string imageName, string? secondaryImageName)
        //{
        //    var imageLocations = new List<string>();

        //    imageLocations.AddRange(ProductMediaInformation.ImageMediasInformation
        //        .Select(media => $"{media.GetAddress}{imageName}"));

        //    if (!string.IsNullOrEmpty(secondaryImageName))
        //    {
        //        imageLocations.AddRange(ProductMediaInformation.SecondaryImageMediasInformation
        //            .Select(media => $"{media.GetAddress}{secondaryImageName}"));
        //    }

        //    //imageLocations.AddRange(galleryImageNames
        //    //    .SelectMany(gallery => ProductGalleryMediaInformation.ImageMediasInformation
        //    //        .Select(media => $"{media.GetAddress}{gallery}")));

        //    return imageLocations;
        //}

        //public static string GetSameAsTag(this List<SocialMediaListDto> socialMedias)
        //{
        //    string items = string.Join("\n",
        //    socialMedias
        //    .ConvertAll(a => $@""));
        //    items = items.ReplaceLastOccurrence(',', ' ');
        //    return $@"
        // ""sameAs"" : [
        //    {items}
        //   ]";
        //}

    //    public static string GetImageTag(
    //        this ProductFullInformationDto product,
    //        string httpsDomain)
    //    {
    //        List<string> path = new List<string>();
    //        string primaryImagePath = $"{httpsDomain}{ProductMediaInformation.DetailImage.GetAddress}{product.BaseInformation.ImageName.ConvertImageNameToWebP()}";
    //        path.Add(primaryImagePath);
    //        if (!string.IsNullOrEmpty(product.BaseInformation.SecondaryImageName))
    //        {
    //            string secondaryImagePath = $"{httpsDomain}{ProductMediaInformation.DetailSecondaryImage.GetAddress}{product.BaseInformation.SecondaryImageName.ConvertImageNameToWebP()}";
    //            path.Add(secondaryImagePath);
    //        }
    //        if (product.GalleryImageNames is not null)
    //        {
    //            foreach (var imageName in product.GalleryImageNames)
    //            {
    //                string imagePath = $"{httpsDomain}{ProductGalleryMediaInformation.DetailImage.GetAddress}{imageName.ConvertImageNameToWebP()}";
    //                path.Add(imagePath);
    //            }
    //        }
    //        string imagesCode = string.Join("\n",
    //            path
    //            .ConvertAll(a => $@"
    //        ""{a}"",
    //"));
    //        imagesCode.ReplaceLastOccurrence(',', ' ');
    //        return $@"
    //        ""image"": [
    //         {imagesCode}
    //        ],";
    //    }

    }
}
