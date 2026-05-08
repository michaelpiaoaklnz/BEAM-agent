#!/bin/bash

module load Python/3.11.6-foss-2023a

export HF_HOME=$HOME/00_nesi_projects/aut04563_nobackup/hf_cache
export HF_HUB_CACHE=$HF_HOME/hub

source ~/00_nesi_projects/aut04563_nobackup/venvs/beam-env311/bin/activate

cd ~/ai-agent-exp/BEAM-agent

python -m vllm.entrypoints.openai.api_server \
  --model Qwen/Qwen2.5-Coder-14B-Instruct \
  --download-dir $HF_HOME/hub \
  --gpu-memory-utilization 0.85 \
  --max-model-len 16384 \
  --enforce-eager
