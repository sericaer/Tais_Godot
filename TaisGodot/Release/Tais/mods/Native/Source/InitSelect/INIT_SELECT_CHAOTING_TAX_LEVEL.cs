using Tais.API;

namespace Native.InitSelect
{
    internal class INIT_SELECT_CHAOTING_TAX_LEVEL : Tais.API.InitSelect
    {
        public override IDesc title => DESC("INIT_SELECT_CHAOTING_TAX_LEVEL_TITLE");

        public override IDesc desc => DESC("INIT_SELECT_CHAOTING_TAX_LEVEL_DESC");

        public override InitSelectOption[] options => ARRAY
            (
                OPTION_INIT_SELECT
                (
                    DESC("INIT_SELECT_CHAOTING_TAX_LEVEL_1_DESC"),
                    ARRAY
                    (
                        ASSIGN(INIT_CHAOTING_TAX_LEVEL, 1)
                    )
                )
            );
    }
}