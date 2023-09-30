using Nabeey.Service.DTOs.Certificates;

namespace Nabeey.Service.Interfaces;

public interface ICertificateService
{
    ValueTask<CertificateResultDtoDto> GenerateAsync(CertificateCreationDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CertificateResultDtoDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<CertificateResultDtoDto>> RetriveUserCertificatesAsync(long userId);
    ValueTask<IEnumerable<CertificateResultDtoDto>> RetrieveByQuizIdCertificateAsync(long userId, long quizId);
    ValueTask<IEnumerable<CertificateResultDtoDto>> RetrieveAllAsync();
} 