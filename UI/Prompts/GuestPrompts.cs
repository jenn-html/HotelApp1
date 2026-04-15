using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using ValidationResult = Spectre.Console.ValidationResult;

namespace HotelApp1.UI.Prompts
{
    public class GuestPrompts
    {
        public static string PromptFirstname()
        {

            var newFirstName = AnsiConsole.Prompt(
               new TextPrompt<string>("[yellow]First name:[/]")
                   .Validate(g => g.Length <= 50 && g.Length > 1 ? ValidationResult.Success() : ValidationResult.Error("[red]Minimum 2 character and maximum 50[/]"))
           );
            return newFirstName;
        }
        public static string PromptLastname()
        {
            var newLastName = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]Last name:[/]")
            .Validate(g => g.Length <= 50 && g.Length > 1 ? ValidationResult.Success() : ValidationResult.Error("[red]Minimum 2 character and maximum 50[/]"))
            );
            return newLastName;
        }
        public static string PromptEmail()
        {
            var emailValidator = new EmailAddressAttribute();

            var newEmail = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Email:[/]")
                    .Validate(e =>
                    {
                        if (string.IsNullOrWhiteSpace(e) || e.Length < 2 || e.Length > 100)
                            return ValidationResult.Error("[red]Email must be between 2 and 100 characters[/]");

                        return emailValidator.IsValid(e)
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Invalid email address[/]");
                    })
            );

            return newEmail;
        }

        public static string PromptPhone()
        {
            var phoneValidator = new PhoneAttribute();

            var phone = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Phone (optional):[/]")
                    .Validate(p =>
                    {
                        if (string.IsNullOrWhiteSpace(p))
                            return ValidationResult.Success(); // allow empty (optional)

                        var trimmed = p.Trim();

                        if (trimmed.Length < 5 || trimmed.Length > 30)
                            return ValidationResult.Error("[red]Phone must be between 5 and 30 characters[/]");

                        return phoneValidator.IsValid(trimmed)
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Invalid phone number format[/]");
                    })
            );

            return string.IsNullOrWhiteSpace(phone) ? null : phone.Trim();
        }
        public static string PromptStreet()
        {

            var newStreet = AnsiConsole.Prompt(
    new TextPrompt<string>("[yellow]Street:[/]")
        .Validate(g => g.Length <= 100 ? ValidationResult.Success() : ValidationResult.Error("[red]Minimum 2 character and maximum 100[/]"))
);
            return newStreet;
        }
        public static string PromptPostalcode()
        {
            var newPostalcode = AnsiConsole.Prompt(
   new TextPrompt<string>("[yellow]Postalcode:[/]")
       .Validate(g => g.Length <= 50 && g.Length > 1 ? ValidationResult.Success() : ValidationResult.Error("[red]Minimum 2 character and maximum 50[/]"))
);

            return newPostalcode;
        }
        public static string PromptCity()
        {

            var newCity = AnsiConsole.Prompt(
       new TextPrompt<string>("[yellow]City:[/]")
           .Validate(g => g.Length <= 50 && g.Length > 1 ? ValidationResult.Success() : ValidationResult.Error("[red]Minimum 2 character and maximum 50[/]"))
    );
            return newCity;
        }
        public static string PromptCountry()
        {

            var newCountry = AnsiConsole.Prompt(
       new TextPrompt<string>("[yellow]Country:[/]")
           .Validate(g => g.Length <= 50 && g.Length > 1 ? ValidationResult.Success() : ValidationResult.Error("[red]Minimum 2 character and maximum 50[/]"))
    );
            return newCountry;
        }
    }
}


