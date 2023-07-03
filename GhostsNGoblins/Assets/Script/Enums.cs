using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum EstadoPlayer
{
    Corre,
    Crouch,
    CrouchThrow,
    Escalera,
    Idle,
    Lose,
    Muerte,
    Throw,
    Win
}

public enum EstadoZombi
{
    Entrando,
    Andando,
    Saliendo
}

public enum EstadoCiclope
{
    Andando,
    Quieto,
    Saltando
}