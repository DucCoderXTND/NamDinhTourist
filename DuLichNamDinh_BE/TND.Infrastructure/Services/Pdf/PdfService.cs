using NReco.PdfGenerator;
using TND.Domain.Interfaces.Services;

namespace TND.Infrastructure.Services.Pdf
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> GeneratePdfFromHtmlAsync(string html, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                var htmlToPdf = new HtmlToPdfConverter();
                return htmlToPdf.GeneratePdf(html);
            }, cancellationToken);
        }
    }
}
