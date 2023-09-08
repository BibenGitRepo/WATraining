namespace WebatrioTest.Helpers
{
    public static class AgeCalculator
    {
        public static int CalculerAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;

            // Ajustez l'âge si la date de naissance n'est pas encore passée cette année
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }
    }
}
