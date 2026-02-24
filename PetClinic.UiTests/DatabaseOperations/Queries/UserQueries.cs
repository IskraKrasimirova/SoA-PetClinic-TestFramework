namespace PetClinic.UiTests.DatabaseOperations.Queries
{
    // Use this as an example. Delete before submitting the exam.
    public static class UserQueries
    {
        public static string DeleteUserByEmail(string email)
        {
            return $@"
                DELETE FROM users
                WHERE email = '{email}';
            ";
        }
    }
}
