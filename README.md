# Orbital Decay Simulator

stage 1 — 1d point-mass drop

stage 2 — vertical free fall with basic drag

stage 3 — circular orbital decay using Euler method

(see above in "PROTOTYPES")

stage 4 — Full 3d RK4 vector propagator: built physics engine using J2 perturbations and Runge-Kutta 4th order numerical integration. This project helps simulates the orbital decay of a satellite due to atmospheric drag.

(see stage 4 code and output csv in "OrbitalDecaySimulator_trajectory_calculation" & "OrbitalDecaySimulator_trajectory data")

(see "OrbitalDecaySimulator" for all unity assets)

** Main breakthrough with stage 4: originally used  first-order Euler integration, satellite went out of control into deep space. After doing some research online, I understood that the Euler integration method used a simple straight-line guess for each time step, that caused the overshooting on the orbital curve.
I used 4th-Order Runge-Kutta numerical integrator: 

$$k_1 = f(t_n, y_n)$$

$$k_2 = f\left(t_n + \frac{h}{2}, y_n + \frac{h}{2}k_1\right)$$

$$k_3 = f\left(t_n + \frac{h}{2}, y_n + \frac{h}{2}k_2\right)$$

$$k_4 = f(t_n + h, y_n + hk_3)$$

Simpson's rule:
$$y_{n+1} = y_n + \frac{h}{6}\left(k_1 + 2k_2 + 2k_3 + k_4\right)$$

$$\text{New Position } (y_{n+1}) = \text{Old Position } (y_n) + h \times \left( \frac{k_1 + 2k_2 + 2k_3 + k_4}{6} \right)$$



