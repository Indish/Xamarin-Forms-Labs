﻿namespace Xamarin.Forms.Labs.Services.Serialization
{
    /// <summary>
    /// The stream serializer.
    /// </summary>
    public abstract class StreamSerializer : ISerializer
    {
        #region ISerializer Members

        /// <summary>
        /// Gets the serialization format.
        /// </summary>
        /// <value>Serialization format.</value>
        public abstract SerializationFormat Format { get; }

        /// <summary>
        /// Cleans memory.
        /// </summary>
        public abstract void Flush();
        #endregion

        #region IByteSerializer Members

        /// <summary>
        /// Serializes object to a string.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize to.</typeparam>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized byte[] of the object.</returns>
        public byte[] SerializeToBytes<T>(T obj)
        {
            return (this as IStreamSerializer).GetSerializedBytes(obj);
        }

        /// <summary>
        /// Deserializes string into an object.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize to.</typeparam>
        /// <param name="data">Serialized object as byte buffer.</param>
        /// <returns>Object of type T.</returns>
        public T Deserialize<T>(byte[] data)
        {
            return (this as IStreamSerializer).DeserializeFromBytes<T>(data);
        }
        #endregion

        #region IStreamSerializer Members

        /// <summary>
        /// Serializes object to a stream.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize.</typeparam>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="stream">Stream to serialize to.</param>
        public abstract void Serialize<T>(T obj, System.IO.Stream stream);

        /// <summary>
        /// Deserializes stream into an object.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize to.</typeparam>
        /// <param name="stream">Stream to deserialize from.</param>
        /// <returns>Object of type T.</returns>
        public abstract T Deserialize<T>(System.IO.Stream stream);
        #endregion

        #region IStringSerializer Members
        /// <summary>
        /// Serializes object to a string.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize.</typeparam>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized string of the object.</returns>
        public string Serialize<T>(T obj)
        {
            return (this as IStreamSerializer).SerializeToString(obj);
        }

        /// <summary>
        /// Deserializes string into an object.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize to.</typeparam>
        /// <param name="data">Serialized object.</param>
        /// <returns>Object of type T.</returns>
        public T Deserialize<T>(string data)
        {
            return (this as IStreamSerializer).DeserializeFromString<T>(data);
        }
        #endregion
    }
}
