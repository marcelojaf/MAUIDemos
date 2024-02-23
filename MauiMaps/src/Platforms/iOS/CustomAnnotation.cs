using MapKit;
using Microsoft.Maui.Maps;
using UIKit;

namespace MauiMaps;

public class CustomAnnotation : MKPointAnnotation
{
    public Guid Identifier { get; set; }
    public UIImage? Image { get; set; }
    public required IMapPin Pin { get; set; }
}