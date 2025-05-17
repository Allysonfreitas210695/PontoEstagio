import { zodResolver } from '@hookform/resolvers/zod';
import { useRouter } from 'next/navigation';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { api } from '../api/api';
import { EyeIcon, EyeOffIcon } from 'lucide-react';

const registerSchema = z.object({
  name: z.string().min(3, 'Nome deve ter pelo menos 3 caracteres'),
  email: z.string().email('E-mail inválido'),
  password: z.string().min(6, 'Senha deve ter pelo menos 6 caracteres'),
  confirmPassword: z.string(),
  profile: z.enum(['ESTAGIARIO', 'SUPERVISOR']),
}).refine((data) => data.password === data.confirmPassword, {
  message: 'Senhas não coincidem',
  path: ['confirmPassword'],
});

export type RegisterFormData = z.infer<typeof registerSchema>;

export const useRegister = () => {
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormData>({
    resolver: zodResolver(registerSchema),
  });

  const onSubmit = async (data: RegisterFormData) => {
    setIsLoading(true);
    setErrorMessage('');
    setSuccessMessage('');

    try {
      // 1. Cadastra o usuário
      await api.post('/api/register', {
        nome: data.name,
        email: data.email,
        senha: data.password,
        perfil: data.profile,
      });

      // 2. Autentica o usuário após o cadastro
      const loginResponse = await api.post('/login', {
        email: data.email,
        senha: data.password,
      });
      //vericar se está correto
      const { accessToken, refreshToken } = loginResponse.data as { accessToken: string; refreshToken: string };

      // 3. Salva os tokens
      localStorage.setItem('accessToken', accessToken);
      localStorage.setItem('refreshToken', refreshToken);

      // 4. Redireciona
      setSuccessMessage('Cadastro e login realizados com sucesso! Redirecionando...');
      setTimeout(() => {
        router.push('/dashboard');
      }, 2000);
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    } catch (error: any) {
      if (error.response?.data?.message) {
        setErrorMessage(error.response.data.message);
      } else {
        setErrorMessage('Erro ao cadastrar ou autenticar usuário.');
      }
    } finally {
      setIsLoading(false);
    }
  };

  return {
    register,
    handleSubmit: handleSubmit(onSubmit),
    errors,
    isLoading,
    errorMessage,
    successMessage,
    showPassword,
    setShowPassword,
    showConfirmPassword,
    setShowConfirmPassword,
    EyeIcon,
    EyeOffIcon,
  };
};
