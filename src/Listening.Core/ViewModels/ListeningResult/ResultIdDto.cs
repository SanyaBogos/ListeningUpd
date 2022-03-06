using System;
using Newtonsoft.Json;

namespace Listening.Core.ViewModels.ListeningResult
{
    public class ResultIdDto : IEquatable<ResultIdDto>
    {
        [JsonIgnore]
        public long UserId { get; set; }
        public string TextId { get; set; }
        public char Mode { get; set; }

        public bool Equals(ResultIdDto other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return other != null && UserId == other.UserId
                        && TextId == other.TextId
                        && Mode == other.Mode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ResultIdDto);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17, prime = 23;

                hash = hash * prime + UserId.GetHashCode();
                hash = hash * prime + TextId.GetHashCode();
                hash = hash * prime + Mode.GetHashCode();

                return hash;
            }
        }
    }
}
