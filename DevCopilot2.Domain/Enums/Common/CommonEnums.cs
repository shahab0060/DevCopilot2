using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevCopilot2.Domain.Enums.Common
{
    #region Base Change Entity Result

    public enum BaseChangeEntityResult
    {
        Success,
        NotFound,
        Exists,
        Invalid
    }

    #endregion

}
