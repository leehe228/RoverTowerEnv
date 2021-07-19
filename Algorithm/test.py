import mlagents
import numpy as np
from mlagents_envs.environment import UnityEnvironment

from UnityGymWrapper import GymEnv

env = GymEnv(name="../Build/RoverTower")


for episode in range(10):

    obs = env.reset()

    for step in range(30):
        print("step :", step, end='\r')
        
        action = np.random.rand(8, 5)
        
        obs, reward, done, info = env.step(action)
