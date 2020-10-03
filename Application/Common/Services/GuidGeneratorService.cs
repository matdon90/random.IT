using Application.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Common.Services
{
    /// <summary>
    /// Guid generator based on RFC 4122 algorithm
    /// </summary>
    public class GuidGeneratorService : IGuidGenerator
    {
        #region PPRIVATE variables
        private readonly Random _random;

        private const int _guidByteArraySize = 16;

        private const byte _timestampByteIndex = 0;
        private const byte _guidClockSequenceByteIndex = 8;
        private const byte _nodeByteIndex = 10;

        private const int _variantByteIndex = 8;
        private const int _variantByteMask = 0x3f;
        private const int _variantByteShift = 0x80;

        private const int _versionByteIndex = 7;
        private const int _versionByteMask = 0x0f;
        private const int _versionByteShift = 4;
        #endregion

        public GuidGeneratorService()
        {
            _random = new Random();
        }

        /// <summary>
        /// Random node bytes generation
        /// </summary>
        /// <returns></returns>
        private byte[] RandomNodeBytes()
        {
            var nodeBytes = new byte[6];
            _random.NextBytes(nodeBytes);

            return nodeBytes;
        }

        /// <summary>
        /// Guid generator using version 1 of Guid generation algorithm based on date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
		private Guid GenerateGuidV1(DateTime date)
		{
            byte[] guidByteArray = new byte[_guidByteArraySize];
            byte[] timestamp = BitConverter.GetBytes(date.Ticks - new DateTime(1582, 10, 15, 0, 0, 0).Ticks);
            byte[] clockSequence = BitConverter.GetBytes(Convert.ToInt16(Environment.TickCount % Int16.MaxValue));
            byte[] nodeId = RandomNodeBytes();

			//insterting timestamp bytes into GUID
			Array.Copy(timestamp, 0, guidByteArray, _timestampByteIndex, Math.Min(8, timestamp.Length));

			//inserting clock sequence bytes into GUID
			Array.Copy(clockSequence, 0, guidByteArray, _guidClockSequenceByteIndex, Math.Min(2, clockSequence.Length));

			//inserting node ID bytes into GUID
			Array.Copy(nodeId, 0, guidByteArray, _nodeByteIndex, Math.Min(6, nodeId.Length));

            //setting the version
            guidByteArray[_versionByteIndex] &= (byte)_versionByteMask;
            guidByteArray[_versionByteIndex] |= (byte)(0x01 << _versionByteShift);

            //setting the variant
            guidByteArray[_variantByteIndex] &= (byte)_variantByteMask;
            guidByteArray[_variantByteIndex] |= (byte)_variantByteShift;

			return new Guid(guidByteArray);
		}

        public Guid GuidGenerateSingle(DateTime date)
        {
            return GenerateGuidV1(date);
        }

        public IEnumerable<Guid> GuidGenerateMultiple(DateTime date, int numberOfGuid)
        {
            List<Guid> guids = new List<Guid>();
            for (int i = 0; i < numberOfGuid; i++)
            {
                guids.Add(GenerateGuidV1(date));
            };
            return guids;
        }
    }
}
