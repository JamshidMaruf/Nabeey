using Nabeey.Service.DTOs.Certificates;

namespace Nabeey.Service.Interfaces;

public interface ICertificateService
{
    ValueTask<CertificateResultDto> GenerateAsync(CertificateCreationDto dto);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CertificateResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<CertificateResultDto>> RetriveUserCertificatesAsync(long userId);
    ValueTask<IEnumerable<CertificateResultDto>> RetrieveByQuizIdCertificateAsync(long userId, long quizId);
    ValueTask<IEnumerable<CertificateResultDto>> RetrieveAllAsync();
} 