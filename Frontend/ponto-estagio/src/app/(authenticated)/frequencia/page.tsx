"use client";

import { useState, useEffect } from "react";
import styles from "./RegistroFrequencia.module.css";
import { toast } from "sonner";
import { Calendar, Clock } from "lucide-react";

export default function RegistroFrequencia() {
  const [data, setData] = useState("");
  const [entrada, setEntrada] = useState("");
  const [saida, setSaida] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const hoje = new Date().toISOString().split("T")[0];
    setData(hoje);
  }, []);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    if (!entrada || !saida) {
      toast.error("Preencha os horários corretamente!");
      return;
    }

    if (entrada >= saida) {
      toast.error("O horário de entrada deve ser antes do horário de saída.");
      return;
    }

    try {
      setLoading(true);

      const res = await fetch("http://localhost:5019/frequencia", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
        body: JSON.stringify({
          data,
          horaEntrada: entrada,
          horaSaida: saida,
        }),
      });

      const responseData = await res.json();

      if (res.ok) {
        toast.success("Frequência registrada com sucesso!");
        setEntrada("");
        setSaida("");
      } else {
        toast.error(responseData.message || "Erro ao registrar frequência.");
      }
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    } catch (error) {
      toast.error("Erro ao conectar com o servidor.");
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className={styles.container}>
      <h1 className={styles.title}>Registrar Frequência</h1>
      <form onSubmit={handleSubmit} className={styles.form}>
        <div className={styles.field}>
          <label className={styles.label}>
            <Calendar size={18} /> Data
          </label>
          <input
            type="date"
            value={data}
            disabled
            className={styles.input}
          />
        </div>

        <div className={styles.field}>
          <label className={styles.label}>
            <Clock size={18} /> Hora de Entrada
          </label>
          <input
            type="time"
            value={entrada}
            onChange={(e) => setEntrada(e.target.value)}
            className={styles.input}
            required
          />
        </div>

        <div className={styles.field}>
          <label className={styles.label}>
            <Clock size={18} /> Hora de Saída
          </label>
          <input
            type="time"
            value={saida}
            onChange={(e) => setSaida(e.target.value)}
            className={styles.input}
            required
          />
        </div>

        <button type="submit" className={styles.button} disabled={loading}>
          {loading ? "Enviando..." : "Registrar"}
        </button>
      </form>
    </div>
  );
}
