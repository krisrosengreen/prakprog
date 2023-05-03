from scipy.integrate import odeint
import numpy as np

# u'= x
# x'=-u

def test_ode(y, t):
    x = y[0]
    u = y[1]
    return -np.array([-u, x])

res = odeint(test_ode, y0=np.array([1,1]), t=np.linspace(1, 15))
print(res)
