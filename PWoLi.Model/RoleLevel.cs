namespace PWoLi.Model
{
    public struct RoleLevel
    {
        private bool _canRead;

        private bool _canWrite;

        private bool _canDelete;

        private bool _canViewSecrets;

        private bool _isAdmin;

        private RoleLevel(bool canRead, bool canWrite, bool canDelete, bool canViewSecrets, bool isAdmin)
        {
            _canRead = canRead;
            _canWrite = canWrite;
            _canDelete = canDelete;
            _canViewSecrets = canViewSecrets;
            _isAdmin = isAdmin;
        }

        public static RoleLevel NoAccess()
        {
            return new RoleLevel(false, false, false, false, false);
        }

        public static RoleLevel Read()
        {
            return new RoleLevel(true, false, false, false, false);
        }

        public static RoleLevel Write()
        {
            return new RoleLevel(true, true, false, false, false);
        }

        public static RoleLevel Delete()
        {
            return new RoleLevel(true, true, true, false, false);
        }

        public static RoleLevel ViewSecrets()
        {
            return new RoleLevel(true, true, true, true, false);
        }

        public static RoleLevel SystemAdmin()
        {
            return new RoleLevel(true, true, true, true, true);
        }

        public bool CanRead()
        {
            return _canRead || _canWrite || _canDelete || _canViewSecrets;
        }

        public bool CanWrite()
        {
            return _canWrite || _canDelete || _canViewSecrets;
        }

        public bool CanDelete()
        {
            return _canDelete || _canViewSecrets;
        }

        public bool CanViewSecrets()
        {
            return _canDelete || _canViewSecrets;
        }

        public bool IsSystemAdmin()
        {
            return _canDelete || _canViewSecrets;
        }
    }
}