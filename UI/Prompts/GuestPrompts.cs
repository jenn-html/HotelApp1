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
                   .Validate(g =>
                   {
                       if (string.IsNullOrWhiteSpace(g))
                           return ValidationResult.Error("[red]First name is required[/]");
                       var trimmed = g.Trim();
                       return trimmed.Length >= 2 && trimmed.Length <= 50
                           ? ValidationResult.Success()
                           : ValidationResult.Error("[red]Minimum 2 characters and maximum 50[/]");
                   })
           );
            return newFirstName.Trim();
        }

        public static string PromptLastname()
        {
            var newLastName = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Last name:[/]")
                    .Validate(g =>
                    {
                        if (string.IsNullOrWhiteSpace(g))
                            return ValidationResult.Error("[red]Last name is required[/]");
                        var trimmed = g.Trim();
                        return trimmed.Length >= 2 && trimmed.Length <= 50
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Minimum 2 characters and maximum 50[/]");
                    })
            );
            return newLastName.Trim();
        }

        public static string PromptEmail()
        {
            var emailValidator = new EmailAddressAttribute();

            var newEmail = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Email:[/]")
                    .Validate(e =>
                    {
                        if (string.IsNullOrWhiteSpace(e))
                            return ValidationResult.Error("[red]Email is required[/]");
                        var trimmed = e.Trim();
                        if (trimmed.Length < 4 || trimmed.Length > 100)
                            return ValidationResult.Error("[red]Email must be between 4 and 100 characters[/]");

                        return emailValidator.IsValid(trimmed)
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Invalid email address[/]");
                    })
            );

            return newEmail.Trim();
        }

        public static string? PromptPhone()
        {
            var phoneValidator = new PhoneAttribute();

            var phone = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Phone (optional):[/]")
                .AllowEmpty()
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

            var result = string.IsNullOrWhiteSpace(phone) ? null : phone.Trim();
            return result;
        }

        public static string PromptStreet()
        {
            var newStreet = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Street:[/]")
                    .Validate(g =>
                    {
                        if (string.IsNullOrWhiteSpace(g))
                            return ValidationResult.Error("[red]Street is required[/]");
                        var trimmed = g.Trim();
                        return trimmed.Length >= 2 && trimmed.Length <= 100
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Minimum 2 characters and maximum 100[/]");
                    })
            );
            return newStreet.Trim();
        }

        public static string PromptPostalcode()
        {
            var newPostalcode = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Postalcode:[/]")
                    .Validate(g =>
                    {
                        if (string.IsNullOrWhiteSpace(g))
                            return ValidationResult.Error("[red]Postal code is required[/]");
                        var trimmed = g.Trim();
                        return trimmed.Length >= 2 && trimmed.Length <= 50
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Minimum 2 characters and maximum 50[/]");
                    })
            );

            return newPostalcode.Trim();
        }

        public static string PromptCity()
        {
            var newCity = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]City:[/]")
                    .Validate(g =>
                    {
                        if (string.IsNullOrWhiteSpace(g))
                            return ValidationResult.Error("[red]City is required[/]");
                        var trimmed = g.Trim();
                        return trimmed.Length >= 2 && trimmed.Length <= 50
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Minimum 2 characters and maximum 50[/]");
                    })
            );
            return newCity.Trim();
        }

        public static string PromptCountry()
        {
            var newCountry = AnsiConsole.Prompt(
                new TextPrompt<string>("[yellow]Country:[/]")
                    .Validate(g =>
                    {
                        if (string.IsNullOrWhiteSpace(g))
                            return ValidationResult.Error("[red]Country is required[/]");
                        var trimmed = g.Trim();
                        return trimmed.Length >= 2 && trimmed.Length <= 50
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]Minimum 2 characters and maximum 50[/]");
                    })
            );
            return newCountry.Trim();
        }
    }
}


