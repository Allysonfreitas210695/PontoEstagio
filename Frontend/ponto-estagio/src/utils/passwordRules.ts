export type PasswordRule = {
  label: string;
  isValid: boolean;
};

export function getPasswordRules(password: string): PasswordRule[] {
  return [
    {
      label: "Caractere especial",
      isValid: /[@#$!%*?&]/.test(password),
    },
    {
      label: "Letra maiúscula",
      isValid: /[A-Z]/.test(password),
    },
    {
      label: "Letra minúscula",
      isValid: /[a-z]/.test(password),
    },
    {
      label: "Número",
      isValid: /[0-9]/.test(password),
    },
    {
      label: "Oito caracteres",
      isValid: password.length >= 8,
    },
  ];
}

export function isPasswordValid(password: string): boolean {
  return getPasswordRules(password).every((rule) => rule.isValid);
}
