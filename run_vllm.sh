#!/bin/bash -e
#SBATCH --job-name=vllm
#SBATCH --account=aut04721
#SBATCH --partition=milan
#SBATCH --gpus-per-node=a100:1
#SBATCH --cpus-per-task=8
#SBATCH --mem=32G
#SBATCH --time=7-00:00:00
#SBATCH --output=slurm-%j.out

module purge
module load Python/3.11.6-foss-2023a

source ~/00_nesi_projects/aut04563_nobackup/venvs/beam-aider-env/bin/activate
cd ~/ai-agent-exp/BEAM-agent

echo "job=${SLURM_JOB_ID} host=$(hostname)"
echo "CUDA_VISIBLE_DEVICES=${CUDA_VISIBLE_DEVICES}"
nvidia-smi

srun ./start_vllm.sh
