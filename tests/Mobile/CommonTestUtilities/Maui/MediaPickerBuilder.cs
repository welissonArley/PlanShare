using Bogus;
using Moq;

namespace CommonTestUtilities.Maui;
public class MediaPickerBuilder
{
    public static IMediaPicker Build()
    {
        var faker = new Faker();
        var imageUrl = faker.Image.LoremFlickrUrl();

        var mock = new Mock<IMediaPicker>();

        mock.Setup(mediaPicker => mediaPicker.PickPhotoAsync(null)).ReturnsAsync(new FileResult(imageUrl));
        mock.Setup(mediaPicker => mediaPicker.CapturePhotoAsync(null)).ReturnsAsync(new FileResult(imageUrl));

        return mock.Object;
    }
}
