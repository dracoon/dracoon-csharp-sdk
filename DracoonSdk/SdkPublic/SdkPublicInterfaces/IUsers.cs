namespace Dracoon.Sdk {
    /// <summary>
    ///     Handler to do actions on other users.
    /// </summary>
    public interface IUsers {
        /// <summary>
        ///     Get the avatar image of a given user.
        /// </summary>
        /// <param name="userId">The ID of the user for which the avatar should be returned.</param>
        /// <param name="avatarUuid">The corresponding uuid of the current avatar image for the given user.</param>
        /// <returns>The avatar image of the requested user.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        byte[] GetUserAvatar(long userId, string avatarUuid);
    }
}