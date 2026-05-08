#!/bin/bash

# Load Python
module load Python/3.11.6-foss-2023a

# Set cache paths
export HF_HOME=$HOME/00_nesi_projects/aut04563_nobackup/hf_cache
export HF_HUB_CACHE=$HF_HOME/hub
export PIP_CACHE_DIR=$HOME/00_nesi_projects/aut04563_nobackup/pip_cache

# Activate env
source ~/00_nesi_projects/aut04563_nobackup/venvs/beam-env311/bin/activate

# Go to repo
cd ~/ai-agent-exp/BEAM-agent
