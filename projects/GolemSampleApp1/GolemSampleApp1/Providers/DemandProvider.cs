using System;
using System.Collections.Generic;
using System.Text;

namespace GolemSampleApp1.Providers
{
    public class DemandProvider
    {
        public enum NegotiationStageEnum
        {
            Start,
            Negotiate
        }

        public enum ScenarioEnum
        {
            Transcoding,
            Wasm
        }

        public ScenarioEnum Scenario { get; set; }

        public DemandProvider(ScenarioEnum scenario)
        {
            this.Scenario = scenario;
        }

        public string GetDemandConstraints(NegotiationStageEnum stage)
        {
            switch(this.Scenario)
            {
                case ScenarioEnum.Transcoding:
                    switch(stage)
                    {
                        case NegotiationStageEnum.Start:
                            return Resources.Transcoding_Demand_Start;
                        case NegotiationStageEnum.Negotiate:
                            return Resources.Transcoding_Demand_Negotiate;
                        default:
                            return null;
                    }

                case ScenarioEnum.Wasm:
                    switch (stage)
                    {
                        case NegotiationStageEnum.Start:
                            return Resources.Wasm_Demand_Start;
                        case NegotiationStageEnum.Negotiate:
                            return Resources.Wasm_Demand_Negotiate;
                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }

        public string GetDemandProperties(NegotiationStageEnum stage)
        {
            switch (this.Scenario)
            {
                case ScenarioEnum.Transcoding:
                    switch (stage)
                    {
                        case NegotiationStageEnum.Start:
                            return Resources.Transcoding_Props_Start;
                        case NegotiationStageEnum.Negotiate:
                            return Resources.Transcoding_Props_Negotiate;
                        default:
                            return null;
                    }

                case ScenarioEnum.Wasm:
                    switch (stage)
                    {
                        case NegotiationStageEnum.Start:
                            return Resources.Wasm_Props_Start;
                        case NegotiationStageEnum.Negotiate:
                            return Resources.Wasm_Props_Negotiate;
                        default:
                            return null;
                    }

                default:
                    return null;
            }
        }

    }
}
