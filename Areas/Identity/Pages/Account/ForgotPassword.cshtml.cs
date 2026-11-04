// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Manole_Andreea_Madalina_Lab2.Data;

namespace Manole_Andreea_Madalina_Lab2.Areas.Identity.Pages.Account;

public class ForgotPasswordModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailSender _emailSender;

    public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    // Adăugăm proprietățile pentru afișarea linkului pe ecran
    [BindProperty]
    public string? PasswordResetLink { get; set; }
    public bool DisplayResetPasswordLink { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // În mediu local, dacă utilizatorul nu există sau nu e confirmat, 
                // putem totuși să afișăm un mesaj sau să mergem la confirmare.
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code },
                protocol: Request.Scheme)!;

            // Trimitem și prin email (simulat)
            await _emailSender.SendEmailAsync(
                Input.Email,
                "Reset Password",
                $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            // Salvăm linkul în proprietate ca să-l putem vedea direct pe pagină
            PasswordResetLink = callbackUrl;
            DisplayResetPasswordLink = true;

            return Page();
        }

        return Page();
    }
}