using System;

namespace DevCopilot2.Domain.DTOs.Paging
{
    public class Pager
    {
        public static BasePaging Build(int pageId, int allEntitiesCount, int take, int howManyShowPageAfterAndBefore)
        {
            var pageCount = Convert.ToInt32(Math.Ceiling(allEntitiesCount / (double)take));
            pageId = NormalizePageId(pageId,pageCount);
            return new BasePaging
            {
                PageId = pageId,
                AllEntitiesCount = allEntitiesCount,
                TakeEntity = take,
                SkipEntity = (pageId - 1) * take,
                StartPage = pageId - howManyShowPageAfterAndBefore <= 0 ? 1 : pageId - howManyShowPageAfterAndBefore,
                EndPage = pageId + howManyShowPageAfterAndBefore > pageCount ? pageCount : pageId + howManyShowPageAfterAndBefore,
                HowManyShowPageAfterAndBefore = howManyShowPageAfterAndBefore,
                PageCount = pageCount
            };
        }
        
        public static int NormalizePageId(int pageId,int pageCount)
        {
            if (pageId > pageCount)
                pageId = pageCount;
            if (pageId == 0)
                pageId = 1;
            return pageId;
        }
    }
}
