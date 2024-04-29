using System;

namespace TodoApi.ServiceModel.Flow
{
    internal interface IPrechekFlow {
        Task<Tuple<bool, ErrorData?>> Validate();
    }
}