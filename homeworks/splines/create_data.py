import numpy as np
import math

with open("sin.txt", "w") as f:
    for i in np.linspace(0, 2*np.pi, 20):
        f.write(f"{i},{math.sin(i)}\n")
