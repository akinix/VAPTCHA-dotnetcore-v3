// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using iBestRead.Vaptcha;
using iBestRead.Vaptcha.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebAppMvc.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class MyLoginModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IVaptchaClient _vaptchaClient;

        public MyLoginModel(
            SignInManager<IdentityUser> signInManager,
            IVaptchaClient vaptchaClient)
        {
            _signInManager = signInManager;
            _vaptchaClient = vaptchaClient;
        }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [BindProperty(Name = "vaptcha_token")]
        public string VaptchaToken { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // 二次验证
            var verifyResult = await _vaptchaClient.SecondVerifyAsync(VaptchaToken);
            if (verifyResult.Success != VerifyResult.Success)
            {
                ModelState.AddModelError(string.Empty, "人机验证未通过");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(Email, Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return LocalRedirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "登录失败,用户名或密码错误.");
                return Page();
            }

        }

    }

}